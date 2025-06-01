namespace GrooveNest.Domain.DTOs.RatingDTOs
{
    public class RatingResponseDto
    {
        public int Id { get; set; }
        public int Stars { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid TrackId { get; set; }
        public string TrackTitle { get; set; } = string.Empty;

        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
