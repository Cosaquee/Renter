using System.ComponentModel.DataAnnotations;

namespace Models.Models
{
    public class Role
    {
        public long Id { get; set; }

        [Required]
        [StringLength(32)]
        public string Name { get; set; }
    }
}