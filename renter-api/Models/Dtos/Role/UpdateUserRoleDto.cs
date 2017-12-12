using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Dtos.Role
{
    public class UpdateUserRoleDto
    {
        [Required]
        public long Id {get; set;}
    }
}