using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Dtos.Role
{
    public class UpdateUserRoleDto
    {
        [Required]
        public int ID {get; set;}
    }
}