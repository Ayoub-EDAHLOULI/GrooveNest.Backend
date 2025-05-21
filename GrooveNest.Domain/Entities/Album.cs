namespace GrooveNest.Domain.Entities
{
    public class Album
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public string? CoverUrl { get; set; }


        // --------------------------------------------------- //
        // ---------------- Relationship Area ---------------- //
        // --------------------------------------------------- //
        public Guid ArtistId { get; set; }
        public Artist Artist { get; set; } = null!;
        public ICollection<Track> Tracks { get; set; } = [];
    }
}
