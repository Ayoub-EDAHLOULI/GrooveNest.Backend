namespace GrooveNest.Domain.DTOs.ArtistDTOs
{
    public class ArtistUpdateDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Bio { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
