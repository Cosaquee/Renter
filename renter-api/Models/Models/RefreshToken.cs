using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class RefreshToken
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime Expire { get; set; }
    }
}
