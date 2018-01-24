﻿using System.ComponentModel.DataAnnotations;

namespace Models.Models
{
    public class BookRating : BaseEntity
    {
        public long Id { get; set; }

        public string ISBN { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Range(1, 10)]
        public int Rate { get; set; }
    }
}