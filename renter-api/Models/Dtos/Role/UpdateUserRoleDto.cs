using System.ComponentModel.DataAnnotations;

namespace Models.Dtos.Role
{
    public class UpdateUserRoleDto
    {
        [Required]
        public long Id { get; set; }
    }
}