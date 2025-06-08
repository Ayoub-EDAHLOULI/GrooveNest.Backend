namespace GrooveNest.Domain.DTOs.ArtistApplicationDTOs
{
    public class ArtistApplicationApprovalDto
    {
        public Guid ApplicationId { get; set; }
        public bool Approve { get; set; }
        public string? RejectionReason { get; set; }
    }
}
