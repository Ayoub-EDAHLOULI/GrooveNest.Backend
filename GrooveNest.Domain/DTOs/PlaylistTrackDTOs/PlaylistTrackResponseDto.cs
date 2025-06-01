namespace GrooveNest.Domain.DTOs.PlaylistTrackDTOs
{
    public class PlaylistTrackResponseDto
    {
        public Guid PlaylistId { get; set; }
        public string PlaylistTitle { get; set; } = string.Empty;

        public Guid TrackId { get; set; }
        public string TrackTitle { get; set; } = string.Empty;

        public int TrackNumber { get; set; }
    }
}
