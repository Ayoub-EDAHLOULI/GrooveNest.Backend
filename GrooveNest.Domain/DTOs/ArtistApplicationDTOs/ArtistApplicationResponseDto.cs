using GrooveNest.Domain.Enums;

namespace GrooveNest.Domain.DTOs.ArtistApplicationDTOs
{
    public class ArtistApplicationResponseDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string StageName { get; set; } = string.Empty;
        public string ArtistBio { get; set; } = string.Empty;
        public List<string> MusicGenres { get; set; } = [];
        public List<string> SampleTrackLinks { get; set; } = [];
        public List<string> SocialLinks { get; set; } = [];
        public DateTime SubmittedAt { get; set; }
        public bool IsApproved { get; set; }
        public ArtistApplicationStatus Status { get; set; }
        public string? RejectionReason { get; set; }
        public DateTime? DateReviewed { get; set; }
    }
}
