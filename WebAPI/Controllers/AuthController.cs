using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetDevPack.Identity.Jwt;
using NetDevPack.Identity.Jwt.Model;
using NetDevPack.Identity.Model;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
	[ApiController]
	[Route("api/account")]
	public class AuthController : Controller
	{
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly AppJwtSettings _appJwtSettings;

		public AuthController(SignInManager<IdentityUser> signInManager,
			UserManager<IdentityUser> userManager,
			IOptions<AppJwtSettings> appJwtSettings)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_appJwtSettings = appJwtSettings.Value;
		}

		[HttpGet]
		public IActionResult Get()
		{
			return Ok("Satus Ok");
		}

		[HttpPost("register")]
		public async Task<ActionResult> Register(RegisterUser registerUser)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var user = new IdentityUser
			{
				UserName = registerUser.Email,
				Email = registerUser.Email,
				EmailConfirmed = true
			};

			var result = await _userManager.CreateAsync(user, registerUser.Password);

			if (result.Succeeded)
				return Ok(GetUserResponse(user.Email));

			return BadRequest(result.Errors);
		}

		[HttpPost("login")]
		public async Task<ActionResult> Login(LoginUser loginUser)
		{
			if (!ModelState.IsValid) 
				return BadRequest(ModelState);

			var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

			if (result.Succeeded)
				return Ok(GetFullJwtToken(loginUser.Email));

			if (result.IsLockedOut)
				return BadRequest("Este usuário está bloqueado.");

			return BadRequest("Dados de acesso inválidos.");
		}

		private UserResponse<string> GetUserResponse(string email)
		{
			return new JwtBuilder()
				.WithUserManager(_userManager)
				.WithJwtSettings(_appJwtSettings)
				.WithEmail(email)
				.WithJwtClaims()
				.WithUserClaims()
				.WithUserRoles()
				.BuildUserResponse();
		}

		private string GetFullJwtToken(string email)
		{
			return new JwtBuilder()
				.WithUserManager(_userManager)
				.WithJwtSettings(_appJwtSettings)
				.WithEmail(email)
				.WithJwtClaims()
				.WithUserClaims()
				.WithUserRoles()
				.BuildToken();
		}
	}
}
