namespace GrooveNest.Domain.DTOs.ArtistApplicationDTOs
{
    public class ArtistApplicationResponseDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string? Message { get; set; }
        public DateTime SubmittedAt { get; set; }
        public bool IsApproved { get; set; }
    }
}
