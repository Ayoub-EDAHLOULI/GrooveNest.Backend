namespace GrooveNest.Domain.DTOs.ArtistDTOs
{
    public class ArtistResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Bio { get; set; }
        public string? UserName { get; set; }
    }
}
