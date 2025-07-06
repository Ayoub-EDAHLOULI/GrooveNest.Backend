using GrooveNest.Domain.DTOs.AlbumDTOs;
using GrooveNest.Domain.DTOs.GenreDTOs;
using GrooveNest.Domain.DTOs.TrackDTOs;

namespace GrooveNest.Domain.DTOs.ArtistDTOs
{
    public class ArtistDetailsResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Bio { get; set; }
        public string? UserName { get; set; }
        public string? ProfilePictureUrl { get; set; }

        public List<TrackResponseDto> Tracks { get; set; } = [];
        public List<AlbumResponseDto> Albums { get; set; } = [];
    }
}
