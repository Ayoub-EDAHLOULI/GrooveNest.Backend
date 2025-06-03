namespace GrooveNest.Domain.DTOs.UserDTOs
{
    public class UserAuthResponseDto
    {
        public string Id { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? ProfilePictureUrl { get; set; }
        public string Token { get; set; } = null!;
    }
}
