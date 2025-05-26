namespace GrooveNest.Domain.DTOs.PlaylistDTOs
{
    public class PlaylistResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsPublic { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid OwnerId { get; set; }
        public string OwnerUserName { get; set; } = "Unknown";
    }
}
