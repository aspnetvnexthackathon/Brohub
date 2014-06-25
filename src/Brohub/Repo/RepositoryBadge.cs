namespace Brohub
{
    public class RepositoryBadge
    {
        public int Id { get; set; }
        public int RepositoryId { get; set; }
        public int BadgeId { get; set; }
        public string UserId { get; set; }
        public int Rank { get; set; }

        public virtual RepositoryBadge Repository { get; set; }

        public virtual Badge Badge { get; set; }

        public virtual User User { get; set; }
    }
}