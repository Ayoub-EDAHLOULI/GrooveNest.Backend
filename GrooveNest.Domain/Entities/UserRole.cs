namespace GrooveNest.Domain.Entities
{
    /// <summary>
    /// Many-to-many join entity between Users and Roles.
    /// Composite primary key on (UserId, RoleId).
    /// </summary>
    public class UserRole
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
    }
}
