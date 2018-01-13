using System.ComponentModel.DataAnnotations;

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

        public long AuthorId { get; set; }

        public long CategoryId { get; set; }
    }
}