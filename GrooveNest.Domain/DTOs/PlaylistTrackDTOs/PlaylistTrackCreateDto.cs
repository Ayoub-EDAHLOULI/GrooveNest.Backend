namespace GrooveNest.Domain.DTOs.PlaylistTrackDTOs
{
    public class PlaylistTrackCreateDto
    {
        public Guid PlaylistId { get; set; }
        public Guid TrackId { get; set; }
        public int TrackNumber { get; set; }
    }
}
