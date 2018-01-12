using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Dtos.BookRating
{
    public class BookRatingDto
    {
        [Required]
        public string UserID {get; set; }

        [Required]
        public int Rate { get; set; }

        [Required]
        public string ISBN { set; get; }
    }
}