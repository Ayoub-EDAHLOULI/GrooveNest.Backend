namespace GrooveNest.Domain.DTOs.TrackDTOs
{
    public class TrackCreateDto
    {
        public string Title { get; set; } = null!;
        public int DurationSec { get; set; }
        public string AudioUrl { get; set; } = null!;
        public int TrackNumber { get; set; }
        public Guid ArtistId { get; set; }
    }
}
