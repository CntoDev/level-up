using Roster.Core.Domain;

namespace Roster.Core.Storage
{
    public class FilterMembers : ISpecification<Member>
    {
        private readonly string _nickname;

        public FilterMembers(string nickname)
        {
            _nickname = nickname;
        }

        public bool Predicate(Member arg)
        {
            string searchFor = _nickname ?? "";
            return arg.Nickname.Contains(searchFor);
        }
    }
}