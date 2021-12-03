using System;

namespace Roster.Core.Domain
{
    public class Rank : AggregateRoot
    {
        private int _id;

        private Rank(string name)
        {
            Name = name;
        }

        public RankId Id => new RankId(_id);

        public string Name { get; private set; }

        public static Rank Create(string name)
        {
            Rank rank = new Rank(name);
            return rank;
        }
    }
}