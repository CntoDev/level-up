using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Saga;
using Roster.Core.Events;
using Roster.Core.Domain;
using Serilog;

namespace Roster.Core.Sagas
{
    public class RecruitmentSaga : ISaga,
        InitiatedBy<MemberCreated>,
        InitiatedBy<MemberRejoined>,
        Observes<ModsChecked, RecruitmentSaga>,
        Observes<BootcampCompleted, RecruitmentSaga>,
        Observes<EnoughEventsAttended, RecruitmentSaga>,
        Observes<AutomaticDischargeToggled, RecruitmentSaga>,
        Orchestrates<RecruitTrialExpired>,
        Orchestrates<RecruitAssessmentExpired>
    {
        const string FailedAssessment = "Recruit failed to pass mods check or bootcamp in time.";
        const string TrialExpired = "Recruit trial has expired.";

        public Guid CorrelationId { get; set; }

        public string Nickname { get; set; }

        public bool AutomaticDischarge { get; set; }

        public DateTime? RecruitmentStartDate { get; set; }

        public DateTime? ModsCheckDate { get; set; }

        public DateTime? BootcampCompletionDate { get; set; }

        public bool EnoughAttendedEvents { get; set; }

        public bool? TrialSucceeded { get; set; }

        Expression<Func<RecruitmentSaga, ModsChecked, bool>> Observes<ModsChecked, RecruitmentSaga>.CorrelationExpression => (saga, message) => saga.Nickname.Equals(message.Nickname);

        Expression<Func<RecruitmentSaga, BootcampCompleted, bool>> Observes<BootcampCompleted, RecruitmentSaga>.CorrelationExpression => (saga, message) => saga.Nickname.Equals(message.Nickname);

        Expression<Func<RecruitmentSaga, EnoughEventsAttended, bool>> Observes<EnoughEventsAttended, RecruitmentSaga>.CorrelationExpression => (saga, message) => saga.Nickname.Equals(message.Nickname);

        Expression<Func<RecruitmentSaga, AutomaticDischargeToggled, bool>> Observes<AutomaticDischargeToggled, RecruitmentSaga>.CorrelationExpression => (saga, message) => saga.Nickname.Equals(message.Nickname);

        public Task Consume(ConsumeContext<MemberCreated> context)
        {
            Nickname = context.Message.Nickname;
            RecruitmentStartDate = context.Message.JoinDate;
            AutomaticDischarge = true;
            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<ModsChecked> context)
        {
            if (IsSagaFinished())
                return Task.CompletedTask;

            if (ModsCheckDate != null)
                return Task.CompletedTask;

            ModsCheckDate = DateTime.UtcNow;
            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<BootcampCompleted> context)
        {
            if (IsSagaFinished())
                return Task.CompletedTask;

            if (BootcampCompletionDate != null)
                return Task.CompletedTask;

            BootcampCompletionDate = DateTime.UtcNow;
            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<AutomaticDischargeToggled> context)
        {
            if (IsSagaFinished())
                return Task.CompletedTask;

            AutomaticDischarge = !AutomaticDischarge;

            // if recruit's time is up and interviewer turned on automatic discharge, goodbye recruit
            bool shouldDischarge = (AutomaticDischarge, TrialSucceeded) switch
            {
                (true, false) => true,
                _ => false
            };

            if (shouldDischarge)
                context.Send(new DischargeRecruit(Nickname, TrialExpired));

            return Task.CompletedTask;
        }

        public async Task Consume(ConsumeContext<RecruitTrialExpired> context)
        {
            /*
             * Due to issue with RabbitMq timers, events must be processed in shorter time periods.
             */
            ExpirationDate expirationDate = new(context.Message.EndDate);

            if (!expirationDate.Expired)
            {                
                RecruitTrialExpired @event = context.Message;
                RecruitTrialExpired republishedEvent = new (@event.Nickname, @event.Days, @event.EndDate, expirationDate.NextCheckinDate, @event.CorrelationId);
                Log.Information("Republishing event {@republishedEvent}.", republishedEvent);
                await context.SchedulePublish(republishedEvent.ScheduledForDate, republishedEvent);
                return;
            }

            Log.Information("Recruit {nickname} trial expired.", context.Message.Nickname);

            if (TrialSucceeded.HasValue)
                return;

            TrialSucceeded = IsTrialSuccessfull();

            if (TrialSucceeded.Value)
                await context.Publish(new RecruitPromoted(context.Message.Nickname));
            else if (AutomaticDischarge)
                await context.Send(new DischargeRecruit(context.Message.Nickname, TrialExpired));
        }

        public Task Consume(ConsumeContext<RecruitAssessmentExpired> context)
        {
            if (ModsCheckDate.HasValue && BootcampCompletionDate.HasValue)
                return Task.CompletedTask; // do nothing

            Log.Information("Recruit assessment failed for {nickname}.", context.Message.Nickname);

            // Think this one should be immediate discharge, recruit failed to do mod check + bootcamp in two weeks
            TrialSucceeded = false;
            context.Send(new DischargeRecruit(Nickname, FailedAssessment));
            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<MemberRejoined> context)
        {
            var message = context.Message;
            Nickname = message.Nickname;

            if (message.Alumni)
            {
                RecruitmentStartDate = ModsCheckDate = BootcampCompletionDate = DateTime.UtcNow;
                EnoughAttendedEvents = true;
                TrialSucceeded = true;
            }
            else
            {
                RecruitmentStartDate = DateTime.UtcNow;
                AutomaticDischarge = true;
            }

            return Task.CompletedTask;
        }

        Task IConsumer<EnoughEventsAttended>.Consume(ConsumeContext<EnoughEventsAttended> context)
        {
            if (IsSagaFinished())
                return Task.CompletedTask;

            EnoughAttendedEvents = true;
            bool trialSuccess = IsTrialSuccessfull();

            if (trialSuccess)
            {
                TrialSucceeded = true;
                context.Publish(new RecruitPromoted(context.Message.Nickname));
            }

            return Task.CompletedTask;
        }

        public bool IsSagaFinished() => TrialSucceeded != null;

        private bool IsTrialSuccessfull()
        {
            return ModsCheckDate.HasValue && BootcampCompletionDate.HasValue && EnoughAttendedEvents;
        }
    }
}