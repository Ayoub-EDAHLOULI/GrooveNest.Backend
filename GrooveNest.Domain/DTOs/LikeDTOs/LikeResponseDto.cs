namespace GrooveNest.Domain.DTOs.LikeDTOs
{
    public class LikeResponseDto
    {
        public Guid TrackId { get; set; }
        public string TrackTitle { get; set; } = string.Empty;

        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
