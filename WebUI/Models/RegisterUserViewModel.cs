using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
	public class RegisterUserViewModel
	{
		[EmailAddress]
		[Required]
		public string Email { get; set; }
		
		[Required]
		[StringLength(100, MinimumLength = 6)]
		public string Password { get; set; }
		
		[Compare("Password", ErrorMessage = "As senhas não conferem.")]
		public string ConfirmPassword { get; set; }
	}
}
