﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models.Models
{
    public class Subscription
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        [NotMapped]
        public TimeSpan Duration
        {
            get
            {
                return TimeSpan.FromSeconds(Seconds);
            }
            set
            {
                Seconds = value.TotalSeconds;
            }
        }
        public double Seconds { get; set; }
    }
}