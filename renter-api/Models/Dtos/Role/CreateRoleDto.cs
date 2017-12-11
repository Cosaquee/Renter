using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Dtos.Role
{
    public class CreateRoleDto
    {
        [Required]
        [StringLength(32)]
        public string Name { get; set; }
    }
}
