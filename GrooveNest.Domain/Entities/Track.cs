namespace GrooveNest.Domain.Entities
{
    public class Track
    {
        public Guid TrackId { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = null!;
        public int DurationSec { get; set; }
        public string AudioUrl { get; set; } = null!;
        public int TrackNumber { get; set; }


        // --------------------------------------------------- //
        // ---------------- Relationship Area ---------------- //
        // --------------------------------------------------- //
        public Guid ArtistId { get; set; }
        public Artist Artist { get; set; } = null!;

        public Guid? AlbumId { get; set; }
        public Album? Album { get; set; } // Nullable for tracks not in an album

        public ICollection<TrackGenre> TrackGenres { get; set; } = [];
        public ICollection<PlaylistTrack> PlaylistTracks { get; set; } = [];
        public ICollection<Comment> Comments { get; set; } = [];
        public ICollection<Like> Likes { get; set; } = [];
    }
}
