﻿namespace GrooveNest.Domain.DTOs.UserRoleDTOs
{
    public class UserRoleResponseDto
    {
        public Guid UserId { get; set; }
        public int RoleId { get; set; }
        public string? UserName { get; set; }
        public string? RoleName { get; set; }
    }
}
