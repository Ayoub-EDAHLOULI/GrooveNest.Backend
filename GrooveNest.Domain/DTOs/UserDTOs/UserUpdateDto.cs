﻿using GrooveNest.Domain.Enums;

namespace GrooveNest.Domain.DTOs.UserDTOs
{
    public class UserUpdateDto
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public UserStatus? Status { get; set; }
    }
}
