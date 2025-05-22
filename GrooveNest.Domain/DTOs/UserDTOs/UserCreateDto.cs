namespace GrooveNest.Domain.DTOs.UserDTOs
{
    public class UserCreateDto
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
