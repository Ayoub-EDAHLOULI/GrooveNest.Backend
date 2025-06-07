using GrooveNest.Domain.Enums;

namespace GrooveNest.Domain.Entities
{
    /// <summary>
    /// Core account record for listeners, artists, and admins.
    /// </summary>
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public UserStatus Status { get; set; } = UserStatus.Active;


        // --------------------------------------------------- //
        // ---------------- Relationship Area ---------------- //
        // --------------------------------------------------- //

        public Artist? Artist { get; set; } // Nullable for non-artist users

        public ICollection<UserRole> UserRoles { get; set; } = [];
        public ICollection<Playlist> Playlists { get; set; } = [];
        public ICollection<Comment> Comments { get; set; } = [];
        public ICollection<Like> Likes { get; set; } = [];
        public ICollection<Rating> Ratings { get; set; } = [];
    }
}
