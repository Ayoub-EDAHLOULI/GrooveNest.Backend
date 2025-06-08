namespace GrooveNest.Domain.DTOs.ArtistApplicationDTOs
{
    public class ArtistApplicationCreateDto
    {
        public Guid UserId { get; set; }
        public string ArtistBio { get; set; } = string.Empty;
    }
}
