using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Models
{
    public class RentMovie : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long MovieId { get; set; }

        public virtual Movie Movie { get; set; }

        [Required]
        [StringLength(256)]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public DateTime From { get; set; }

        [Required]
        public DateTime To { get; set; }
    }
}