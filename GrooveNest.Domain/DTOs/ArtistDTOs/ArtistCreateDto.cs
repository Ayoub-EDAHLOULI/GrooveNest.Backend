namespace GrooveNest.Domain.DTOs.ArtistDTOs
{
    public class ArtistCreateDto
    {
        public string Name { get; set; } = null!;
        public string? Bio { get; set; }
        public string? AvatarUrl { get; set; }
        public Guid? UserId { get; set; }  // optional: if created by a user
    }
}
