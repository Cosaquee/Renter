using System.ComponentModel.DataAnnotations;

namespace Models.Dtos.Book
{
    public class CoversDTO
    {
        [Required]
        public string CoverURL { get; set; }

        [Required]
        public string ResizedCoverURL { get; set; }

        [Required]
        public long ID { get; set; } 
    }
}