using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Models
{
    public class Role
    {
        public int Id { get; set; }
        [Required]
        [StringLength(32)]
        public string Name { get; set; }
    }
}
