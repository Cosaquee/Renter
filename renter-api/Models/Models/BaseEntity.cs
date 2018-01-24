using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    public class BaseEntity
    {
        [Required]
        public DateTime CreatedDate { get; set; }
    }
}