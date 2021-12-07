namespace Roster.Core.Domain
{
    public record RankId(int Id) {
        public static RankId Recruit => new(1);
        public static RankId Grunt => new(2);
        public static RankId Reservist => new(3);
        public static RankId Specialist => new(4);
        public static RankId Corporal => new(5);
        public static RankId StaffSergeant => new(6);
        public static implicit operator int(RankId rankId) => rankId.Id;
    }
}