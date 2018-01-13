using System;

namespace Models.Models
{
    public class RefreshToken
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public DateTime Expire { get; set; }
    }
}