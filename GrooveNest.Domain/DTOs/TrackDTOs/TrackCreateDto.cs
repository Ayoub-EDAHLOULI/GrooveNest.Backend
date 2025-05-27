using Microsoft.AspNetCore.Http;

namespace GrooveNest.Domain.DTOs.TrackDTOs
{
    public class TrackCreateDto
    {
        public string Title { get; set; } = null!;
        public int DurationSec { get; set; }
        public string AudioUrl { get; set; } = null!;
        public int TrackNumber { get; set; } = 1; // Default to 1 if not specified
        public Guid ArtistId { get; set; }
        public Guid AlbumId { get; set; }

        public IFormFile AudioFile { get; set; } = null!;
    }
}
