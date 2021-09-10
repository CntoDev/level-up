using Roster.Core.Domain;

namespace Roster.Core.Storage
{
    public class MembersByEmail : ISpecification<Member>
    {
        private readonly string _email;

        public MembersByEmail(string email)
        {
            _email = email;
        }

        public bool Predicate(Member arg)
        {
            return arg.Email.Equals(_email);
        }
    }
}