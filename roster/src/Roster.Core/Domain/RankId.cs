namespace Roster.Core.Domain
{
    public record RankId(int Id) {
        public static RankId Recruit => new RankId(1);
        public static RankId Grunt => new RankId(2);
        public static RankId Reservist => new RankId(3);
        public static RankId Specialist => new RankId(4);
        public static RankId Corporal => new RankId(5);
        public static RankId StaffSergeant => new RankId(6);
    }
}