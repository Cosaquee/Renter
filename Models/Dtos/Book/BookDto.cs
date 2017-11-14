using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Dtos.Book
{
    public class BookDto
    {
        [Required]
        [StringLength(128)]
        public string Title { get; set; }
        [Required]
        [StringLength(13)]
        public string ISBN { get; set; }
        public int AuthorId { get; set; }
    }
}
