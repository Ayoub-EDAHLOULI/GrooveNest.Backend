using GrooveNest.Domain.DTOs.GenreDTOs;
using GrooveNest.Domain.DTOs.PlaylistDTOs;
using GrooveNest.Domain.Enums;

namespace GrooveNest.Domain.DTOs.UserDTOs
{
    public class UserResponseDetails
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public UserStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> Roles { get; set; } = [];
        public List<GenreResponseDto>? FavoriteGenres { get; set; } = [];
        public List<PlaylistResponseDto> Playlists { get; set; } = [];
    }
}
