using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(128)]
        public string Title { get; set; }
        [Required]
        [StringLength(13)]
        public string ISBN { get; set; }//Podobno teraz 13 znakw https://pl.wikipedia.org/wiki/International_Standard_Book_Number
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }

    }
}
