using Microsoft.AspNetCore.Http;

namespace GrooveNest.Domain.DTOs.AlbumDTOs
{
    public class AlbumUpdateDto
    {
        public string? Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public IFormFile? Cover { get; set; }
    }
}
