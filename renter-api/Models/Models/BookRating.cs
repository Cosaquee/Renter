using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Models
{
    public class BookRating
    {
        public long Id { get; set; }
        public string BookTitle { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        [Range(1, 10)]
        public int Rate { get; set; }
    }
}
