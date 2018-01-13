using System.ComponentModel.DataAnnotations;

namespace Models.Dtos.Role
{
    public class CreateRoleDto
    {
        [Required]
        [StringLength(32)]
        public string Name { get; set; }
    }
}