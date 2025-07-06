using GrooveNest.Domain.DTOs.TrackDTOs;

namespace GrooveNest.Domain.DTOs.AlbumDTOs
{
    public class AlbumResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public string? CoverUrl { get; set; }
        public Guid ArtistId { get; set; }
        public string ArtistName { get; set; } = null!;

        public List<TrackResponseDto> Tracks { get; set; } = [];
    }
}
