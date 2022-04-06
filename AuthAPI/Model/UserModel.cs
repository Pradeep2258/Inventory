using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Model
{
    public class UserModel
    {
        [Required]
        public string LoginID { get; set; }
        [Required]
        public string Password { get; set; }

        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public int UserRole { get; set; }
    }

    public class UserLoginModel
    {
        [Required]
        public string LoginID { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class JwtUserDetails
    {
        public string UserID { get; set; }
        public string Username { get; set; }
        public int UserRole { get; set; }
    }
}
