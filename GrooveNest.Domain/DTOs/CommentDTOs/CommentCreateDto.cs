namespace GrooveNest.Domain.DTOs.CommentDTOs
{
    public class CommentCreateDto
    {
        public string Content { get; set; } = null!;
        public Guid TrackId { get; set; }
        public Guid UserId { get; set; }
    }
}
