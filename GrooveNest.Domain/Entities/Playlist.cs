namespace GrooveNest.Domain.Entities
{
    public class Playlist
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public bool IsPublic { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        // --------------------------------------------------- //
        // ---------------- Relationship Area ---------------- //
        // --------------------------------------------------- //
        public Guid OwnerId { get; set; }
        public User Owner { get; set; } = null!;

        public ICollection<PlaylistTrack> PlaylistTracks { get; set; } = [];
    }
}
