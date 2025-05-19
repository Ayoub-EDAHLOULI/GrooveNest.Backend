namespace GrooveNest.Domain.Entities
{
    public class Like
    {
        public Guid TrackId { get; set; }
        public Track Track { get; set; } = null!;

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
