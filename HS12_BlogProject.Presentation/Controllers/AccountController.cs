using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using HS12_BlogProject.Common.Models.DTOs;
using HS12_BlogProject.Presentation.APIService;
using HS12_BlogProject.Presentation.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;

namespace HS12_BlogProject.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IApiService _apiService;

		public AccountController(IApiService apiService)
		{
			_apiService = apiService;
		}

		public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
			try
			{
				if (ModelState.IsValid)
				{
					var token = await _apiService.LoginAsync(model);

					if (token != null)
					{


						#region token ı cookie ye atma
						Response.Cookies.Append("access_token", token.Token, new CookieOptions
						{
							HttpOnly = true,
							Secure = true,
							SameSite = SameSiteMode.None,
							Expires = token.expiration
						});
						#endregion



						var handler = new JwtSecurityTokenHandler();
						var jsonToken = handler.ReadToken(token.Token) as JwtSecurityToken;

						var username = jsonToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

						var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, username),
						new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
					};

						var identity = new ClaimsIdentity(claims, "login");
						ClaimsPrincipal principal = new ClaimsPrincipal(identity);
						await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);



						return RedirectToAction("Index", "Genre");
					}
					else
					{
						ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
					}
				}
			}
			catch (Exception ex)
			{

				ModelState.AddModelError("", $"Giriş işlemi başarısız: {ex.Message}");
			}
			return View();
		}


		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterDTO model)
		{
			if (ModelState.IsValid)
			{
				if (await _apiService.RegisterAsync(model))
				{
					return RedirectToAction("Login");
				}
			}
			return View(model);
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();

			Response.Cookies.Delete("access_token");
			return RedirectToAction("Login");
		}
    }
}
