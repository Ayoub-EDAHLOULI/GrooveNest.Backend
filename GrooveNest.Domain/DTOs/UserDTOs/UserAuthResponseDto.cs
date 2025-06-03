namespace GrooveNest.Domain.DTOs.UserDTOs
{
    public class UserAuthResponseDto
    {
        public string Id { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
