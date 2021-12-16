using System.Diagnostics.CodeAnalysis;

namespace Roster.Core.Domain
{
    public class Rank : AggregateRoot
    {
        private int _id = -1;

        private Rank(string name)
        {
            Name = name;
        }

        public RankId Id => new(_id);

        public string Name { get; }

        public static Rank Create(string name)
        {
            Rank rank = new(name);
            return rank;
        }
    }
}