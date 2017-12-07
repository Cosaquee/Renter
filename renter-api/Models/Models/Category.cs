using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Models
{
    public class Category
    {
        public long Id { get; set; }
        [Required]
        [StringLength(64)]
        public string Name { get; set; }
    }
}
