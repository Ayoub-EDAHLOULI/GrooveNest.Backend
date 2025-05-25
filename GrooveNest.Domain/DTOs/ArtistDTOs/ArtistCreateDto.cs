using Microsoft.AspNetCore.Http;

namespace GrooveNest.Domain.DTOs.ArtistDTOs
{
    public class ArtistCreateDto
    {
        public string Name { get; set; } = null!;
        public string? Bio { get; set; }
        public IFormFile? Avatar { get; set; } // This will be used to upload the avatar image
        public Guid? UserId { get; set; }  // optional: if created by a user
    }
}
