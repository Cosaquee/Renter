using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Models
{
    public class RentBook
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long BookId { get; set; }

        public virtual Book Book { get; set; }

        [Required]
        [StringLength(256)]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public DateTime From { get; set; }

        [Required]
        public DateTime To { get; set; }

        public bool Received { get; set; }
    }
}