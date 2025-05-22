namespace GrooveNest.Domain.DTOs.UserDTOs
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
