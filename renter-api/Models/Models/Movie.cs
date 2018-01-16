using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    public class Movie
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        public long DirectorId { get; set; }

        public virtual Director Director { get; set; }

        public long CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [NotMapped]
        public TimeSpan Duration
        {
            get
            {
                return TimeSpan.FromSeconds(Seconds);
            }
            set
            {
                Seconds = value.TotalSeconds;
            }
        }

        public double Seconds { get; set; }

        public string Description { get; set; }

        public string CoverURL { get; set; }

        public virtual ICollection<MovieRating> MovieRatings { get; set; }
    }
}
