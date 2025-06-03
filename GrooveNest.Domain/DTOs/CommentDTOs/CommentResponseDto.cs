namespace GrooveNest.Domain.DTOs.CommentDTOs
{
    public class CommentResponseDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public Guid TrackId { get; set; }
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public string? TrackTitle { get; set; }
    }
}
