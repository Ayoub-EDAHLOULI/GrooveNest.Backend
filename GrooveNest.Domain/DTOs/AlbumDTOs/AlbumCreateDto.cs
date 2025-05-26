using Microsoft.AspNetCore.Http;

namespace GrooveNest.Domain.DTOs.AlbumDTOs
{
    public class AlbumCreateDto
    {
        public string Title { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public Guid ArtistId { get; set; }
        public IFormFile? Cover { get; set; }
    }
}
