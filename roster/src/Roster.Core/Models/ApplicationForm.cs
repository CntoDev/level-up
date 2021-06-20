namespace Roster.Core.Models
{
    public enum ApplicationFormStatus
    {
        Pending,
        Accepted,
        Rejected,
        Invalid
    }

    public class ApplicationForm
    {
        private ApplicationFormDto _data;

        public ApplicationFormStatus status { get; private set; }

        public ApplicationForm(ApplicationFormDto dto)
        {
            this._data = dto;
            if(this.validate()) {
                this.status = ApplicationFormStatus.Pending;
            } else {
                this.status = ApplicationFormStatus.Invalid;
            }
        }

        // TODO: implement validation logic
        public bool validate()
        {
            return true;
        }
    }
}