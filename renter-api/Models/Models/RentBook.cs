using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Models
{

    public enum State {
        READY,
        RENTED,
        ARCHIVED
    }

    public class RentBook : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long BookId { get; set; }

        public virtual Book Book { get; set; }

        public string ISBN { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public DateTime From { get; set; }

        [Required]
        public DateTime To { get; set; }

        public State State {get; set; }

    }
}