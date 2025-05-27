namespace GrooveNest.Domain.DTOs.TrackDTOs
{
    public class TrackResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public int DurationSec { get; set; }
        public string AudioUrl { get; set; } = null!;
        public int TrackNumber { get; set; }

        public Guid ArtistId { get; set; }
        public string ArtistName { get; set; } = null!;

        public Guid? AlbumId { get; set; }
        public string? AlbumTitle { get; set; }
    }
}
