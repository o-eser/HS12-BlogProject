using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HS12_BlogProject.Application.Services.AppUserService;
using HS12_BlogProject.Common.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HS12_BlogProject.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IAppUserService _appUserService;
		private readonly IConfiguration _configuration;

		public AccountController(IAppUserService appUserService, IConfiguration configuration)
		{
			_appUserService = appUserService;
			_configuration = configuration;
		}

		[HttpPost]
		[Route("register")]
		public async Task<IActionResult> Register(RegisterDTO model)
		{
			IdentityResult result = await _appUserService.Register(model);
			return (result.Succeeded) ? Ok(result) : BadRequest(result);
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login(LoginDTO model)
		{
			var user = await _appUserService.Login(model);

			if (user.Succeeded)
			{
				var authClaims = new List<Claim> {
					new Claim(ClaimTypes.Name, model.UserName),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
				};

				var token = GetToken(authClaims);

				return Ok(new
				{
					token = new JwtSecurityTokenHandler().WriteToken(token),
					expiration = token.ValidTo
				});
			}
			else
			{
				return Unauthorized();
			}

		}


		//JWT Token oluşturmak için bir metot hazırlıyoruz bu metot configuration ayarlarını appsetting.json dosyasından alır.
		private JwtSecurityToken GetToken(List<Claim> authClaims)
		{
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:secretKey"]));
			var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken(
				_configuration["JwtSettings:validIssuer"],
				_configuration["JwtSettings:validAudience"],
				authClaims,
				expires: DateTime.UtcNow.AddMinutes(10),
				signingCredentials: signIn);

			return token;
		}
	}
}
