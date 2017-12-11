using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Models
{
    public class User
    {
        [Key]
        [StringLength(256)]
        public string Id { get; set; }
        [Required]
        [StringLength(64)]
        public string Email { get; set; }
        [Required]
        [StringLength(64)]
        public string UserName { get; set; }
        [Required]
        [StringLength(64)]
        public string Password { get; set; }

        public long RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<RentBook> RentBooks { get; set; }
        public virtual ICollection<RentMovie> RentMovies { get; set; }
    }
}
