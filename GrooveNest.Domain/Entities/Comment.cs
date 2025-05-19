namespace GrooveNest.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        // --------------------------------------------------- //
        // ---------------- Relationship Area ---------------- //
        // --------------------------------------------------- //

        public Guid TrackId { get; set; }
        public Track Track { get; set; } = null!;

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
