namespace GrooveNest.Domain.DTOs.LikeDTOs
{
    public class LikeCreateDto
    {
        public Guid TrackId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
