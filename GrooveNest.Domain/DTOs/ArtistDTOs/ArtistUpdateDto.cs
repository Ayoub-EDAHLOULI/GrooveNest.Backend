using Microsoft.AspNetCore.Http;

namespace GrooveNest.Domain.DTOs.ArtistDTOs
{
    public class ArtistUpdateDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Bio { get; set; }
        public IFormFile? Avatar { get; set; } // This will be used to upload the avatar image
    }
}
