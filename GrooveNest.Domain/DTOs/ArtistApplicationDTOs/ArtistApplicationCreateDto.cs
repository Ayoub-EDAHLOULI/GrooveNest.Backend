namespace GrooveNest.Domain.DTOs.ArtistApplicationDTOs
{
    public class ArtistApplicationCreateDto
    {
        public Guid UserId { get; set; }

        public string StageName { get; set; } = string.Empty;

        public string ArtistBio { get; set; } = string.Empty;

        public List<string> MusicGenres { get; set; } = [];

        public List<string> SampleTrackLinks { get; set; } = [];

        public string? InstagramUrl { get; set; }

        public string? TwitterUrl { get; set; }

        public string? YouTubeUrl { get; set; }
    }
}
