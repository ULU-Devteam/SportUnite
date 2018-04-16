using System.ComponentModel.DataAnnotations;

namespace SportUnite.Domain.Models.Identity
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter a name")]
		[StringLength(50, ErrorMessage = "Name must be less than 50 characters long")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter your password")]
        [UIHint("password")]
	    public string Password { get; set; }
		[Phone(ErrorMessage = "Please enter a valid phonenumber")]
        public string Phone { get; set; }
		[Required(ErrorMessage = "Please specify a role for this user")]
        public string Role { get; set; }
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Please enter a valid emailaddress")]
        public string Email { get; set; }
        public string ReturnUrl { get; set; } = "/";
	}
}