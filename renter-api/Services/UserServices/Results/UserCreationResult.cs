using Models.Models;
using System.Collections.Generic;

namespace Services.UserServices.Results
{
    public class UserCreationResult
    {
        public User User { get; set; }

        public List<string> Errors { get; set; }
    }
}