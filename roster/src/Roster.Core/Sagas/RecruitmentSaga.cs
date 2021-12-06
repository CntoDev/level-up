using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Saga;
using Roster.Core.Events;

namespace Roster.Core.Sagas
{
    public class RecruitmentSaga : ISaga,
        InitiatedBy<MemberCreated>,
        Observes<ModsChecked, RecruitmentSaga>,
        Observes<BootcampCompleted, RecruitmentSaga>,
        Observes<EnoughEventsAttended, RecruitmentSaga>,
        Observes<AutomaticDischargeToggled, RecruitmentSaga>,
        Orchestrates<RecruitTrialExpired>,
        Orchestrates<RecruitAssessmentExpired>
    {
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
            if (ModsCheckDate != null)
                return Task.CompletedTask;

            ModsCheckDate = DateTime.UtcNow;
            bool trialSuccess = IsTrialSuccessfull();

            if (trialSuccess)
            {
                TrialSucceeded = trialSuccess;
                context.Publish(new RecruitPromoted(context.Message.Nickname));
            }

            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<BootcampCompleted> context)
        {
            if (BootcampCompletionDate != null)
                return Task.CompletedTask;

            BootcampCompletionDate = DateTime.UtcNow;
            bool trialSuccess = IsTrialSuccessfull();

            if (trialSuccess)
            {
                TrialSucceeded = trialSuccess;
                context.Publish(new RecruitPromoted(context.Message.Nickname));
            }

            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<AutomaticDischargeToggled> context)
        {
            AutomaticDischarge = !AutomaticDischarge;

            // if recruit's time is up and interviewer turned on automatic discharge, goodbye recruit
            bool shouldDischarge = (AutomaticDischarge, TrialSucceeded) switch
            {
                (true, false) => true,
                _ => false
            };

            if (shouldDischarge)
                context.Publish(new RecruitDischarged(Nickname));

            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<RecruitTrialExpired> context)
        {
            if (TrialSucceeded.HasValue)
                return Task.CompletedTask;

            TrialSucceeded = IsTrialSuccessfull();

            if (TrialSucceeded.Value)
                context.Publish(new RecruitPromoted(context.Message.Nickname));
            else if (AutomaticDischarge)
                context.Publish(new RecruitDischarged(context.Message.Nickname));

            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<RecruitAssessmentExpired> context)
        {
            // Think this one should be immediate discharge, recruit failed to do mod check + bootcamp in two weeks
            context.Publish(new RecruitDischarged(Nickname));
            return Task.CompletedTask;
        }

        Task IConsumer<EnoughEventsAttended>.Consume(ConsumeContext<EnoughEventsAttended> context)
        {
            EnoughAttendedEvents = true;
            bool trialSuccess = IsTrialSuccessfull();

            if (trialSuccess)
            {
                TrialSucceeded = trialSuccess;
                context.Publish(new RecruitPromoted(context.Message.Nickname));
            }

            return Task.CompletedTask;
        }

        private bool IsTrialSuccessfull()
        {
            return ModsCheckDate.HasValue && BootcampCompletionDate.HasValue && EnoughAttendedEvents;
        }
    }
}