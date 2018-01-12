using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Models
{
    public class Book
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(128)]
        public string Title { get; set; }
        [Required]
        [StringLength(13)]
        public string ISBN { get; set; } //Podobno teraz 13 znakw https://pl.wikipedia.org/wiki/International_Standard_Book_Number
        public long AuthorId { get; set; }
        public virtual Author Author { get; set; }

        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public string Description { get; set; }

        public virtual ICollection<BookRating> BookRatings { get; set; }

        public bool Rented { get; set; }

        public string CoverURL { get; set; }
    }
}
