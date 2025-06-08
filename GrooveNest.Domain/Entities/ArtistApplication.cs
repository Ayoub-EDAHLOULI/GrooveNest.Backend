using GrooveNest.Domain.Enums;

namespace GrooveNest.Domain.Entities
{
    public class ArtistApplication
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public string StageName { get; set; } = string.Empty;
        public string ArtistBio { get; set; } = string.Empty;
        public List<string> MusicGenres { get; set; } = [];
        public List<string> SampleTrackLinks { get; set; } = [];
        public string? InstagramUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? YouTubeUrl { get; set; }

        public bool IsApproved { get; set; } = false;
        public string Status { get; set; } = "Pending";
        public string? RejectionReason { get; set; }
        public DateTime? DateReviewed { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}
