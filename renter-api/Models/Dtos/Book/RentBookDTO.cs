using System.ComponentModel.DataAnnotations;

namespace Models.Dtos.RentBook
{
    public class RentBookDTO
    {
        [Required]
        public string UserID { get; set; }

        [Required]
        public long BookID { get; set; }

        [Required]
        public int RentDuration { get; set; }
    }
}