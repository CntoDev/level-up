namespace Roster.Core.Domain
{
    public record RankId(int Id) {
        public static RankId Recruit => new RankId(1);
    }
}