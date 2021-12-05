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
        Orchestrates<RecruitTrialExpired>
    {
        public Guid CorrelationId { get; set; }

        public string Nickname { get; set; }

        public DateTime? RecruitmentStartDate { get; set; }

        public DateTime? ModsCheckDate { get; set; }

        public DateTime? BootcampCompletionDate { get; set; }

        public bool EnoughAttendedEvents { get; set; }

        public bool? TrialSucceeded { get; set; }

        Expression<Func<RecruitmentSaga, ModsChecked, bool>> Observes<ModsChecked, RecruitmentSaga>.CorrelationExpression => (saga, message) => saga.Nickname.Equals(message.Nickname);

        Expression<Func<RecruitmentSaga, BootcampCompleted, bool>> Observes<BootcampCompleted, RecruitmentSaga>.CorrelationExpression => (saga, message) => saga.Nickname.Equals(message.Nickname);

        Expression<Func<RecruitmentSaga, EnoughEventsAttended, bool>> Observes<EnoughEventsAttended, RecruitmentSaga>.CorrelationExpression => (saga, message) => saga.Nickname.Equals(message.Nickname);

        public Task Consume(ConsumeContext<MemberCreated> context)
        {
            Nickname = context.Message.Nickname;
            RecruitmentStartDate = context.Message.JoinDate;
            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<ModsChecked> context)
        {
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
            BootcampCompletionDate = DateTime.UtcNow;
            bool trialSuccess = IsTrialSuccessfull();

            if (trialSuccess)
            {
                TrialSucceeded = trialSuccess;
                context.Publish(new RecruitPromoted(context.Message.Nickname));
            }

            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<RecruitTrialExpired> context)
        {
            if (TrialSucceeded.HasValue)
                return Task.CompletedTask;

            TrialSucceeded = IsTrialSuccessfull();

            if (TrialSucceeded ?? false)
                context.Publish(new RecruitPromoted(context.Message.Nickname));
            else
                context.Publish(new RecruitDischarged(context.Message.Nickname));

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