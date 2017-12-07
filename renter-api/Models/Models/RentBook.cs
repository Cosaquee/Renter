using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class RentBook
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
