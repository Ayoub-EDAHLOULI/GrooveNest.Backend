namespace GrooveNest.Domain.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public int Stars { get; set; }
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
