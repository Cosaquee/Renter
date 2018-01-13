﻿using Models.Models;

namespace Authorization
{
    public class AuthToken
    {
        public string AccessToken { get; set; }

        //TotalSeconds
        public double ExpiresIn { get; set; }

        public string CookieName { get; set; }

        public string RefreshToken { get; set; }

        public User User { get; set; }
    }
}