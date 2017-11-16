using System;
using System.Collections.Generic;
using System.Text;

namespace Authorization
{
    public class AuthToken
    {
        public string AccessToken { get; set; }

        //TotalSeconds
        public double ExpiresIn { get; set; }
    }
}
