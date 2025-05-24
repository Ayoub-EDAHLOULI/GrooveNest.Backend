namespace GrooveNest.Domain.Entities
{
    public class ArtistApplication
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsApproved { get; set; } = false;
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}
