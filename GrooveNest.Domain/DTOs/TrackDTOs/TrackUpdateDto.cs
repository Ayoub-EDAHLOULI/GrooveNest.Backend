using Microsoft.AspNetCore.Http;

namespace GrooveNest.Domain.DTOs.TrackDTOs
{
    public class TrackUpdateDto
    {
        public string? Title { get; set; }
        public int? DurationSec { get; set; }
        public string? AudioUrl { get; set; }
        public int? TrackNumber { get; set; }
        public IFormFile? AudioFile { get; set; }
    }
}
