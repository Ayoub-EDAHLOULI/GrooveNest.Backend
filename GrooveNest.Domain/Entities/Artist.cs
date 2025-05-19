namespace GrooveNest.Domain.Entities
{
    public class Artist
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string? Bio { get; set; }
        public string? AvatarUrl { get; set; }


        // --------------------------------------------------- //
        // ---------------- Relationship Area ---------------- //
        // --------------------------------------------------- //
        public Guid? UserId { get; set; }
        public User? User { get; set; }
        public ICollection<Track> Tracks { get; set; } = [];
        public ICollection<Album> Albums { get; set; } = [];
    }
}
