using System.ComponentModel.DataAnnotations;

namespace Models.Dtos.UserCover
{
    public class UserCover
    {
        [Required]
        public string UserID { get; set; }

        [Required]
        public string AvatarUrl { set; get; }
    }
}