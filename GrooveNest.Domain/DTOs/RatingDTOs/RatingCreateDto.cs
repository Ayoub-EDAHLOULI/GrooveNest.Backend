namespace GrooveNest.Domain.DTOs.RatingDTOs
{
    public class RatingCreateDto
    {
        public int Stars { get; set; }
        public Guid TrackId { get; set; }
        public Guid UserId { get; set; }
    }
}
