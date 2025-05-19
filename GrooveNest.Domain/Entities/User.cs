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
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }
}
