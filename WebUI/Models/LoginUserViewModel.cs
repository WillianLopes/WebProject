using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
	public class LoginUserViewModel
	{
		[EmailAddress]
		[Required]
		public string Email { get; set; }
		[Required]
		[StringLength(100, MinimumLength = 6)]
		public string Password { get; set; }
	}
}
