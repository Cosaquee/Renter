﻿using System.ComponentModel.DataAnnotations;

namespace Models.Models
{
    public class Category : BaseEntity
    {
        public long Id { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }
    }
}