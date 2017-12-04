using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.UserServices.Results
{
    public class UserCreationResult
    {
        public User User { get; set; }
        public List<string> Errors { get; set; }
    }
}
