namespace GrooveNest.Domain.Entities
{
    public class PlaylistTrack
    {
        public Guid PlaylistId { get; set; }
        public Playlist Playlist { get; set; } = null!;
        public Guid TrackId { get; set; }
        public Track Track { get; set; } = null!;
        public int TrackNumber { get; set; } // The order of the track in the playlist
    }
}
