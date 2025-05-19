namespace GrooveNest.Domain.Entities
{
    public class Track
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int DurationSec { get; set; }
        public string AudioUrl { get; set; } = null!;
        public int TrackNumber { get; set; }


        // --------------------------------------------------- //
        // ---------------- Relationship Area ---------------- //
        // --------------------------------------------------- //
        public int AlbumId { get; set; }
        public Album Album { get; set; } = null!;
    }
}
