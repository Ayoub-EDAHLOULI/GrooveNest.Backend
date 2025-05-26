namespace GrooveNest.Domain.DTOs.PlaylistDTOs
{
    public class PlaylistCreateDto
    {
        public string Name { get; set; } = null!;
        public bool IsPublic { get; set; } = true;
        public Guid OwnerId { get; set; }
    }
}
