using HS12_BlogProject.Common.Models.DTOs;
using HS12_BlogProject.Application.Services.AuthorService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HS12_BlogProject.API.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorController : ControllerBase
	{
		private readonly IAuthorService _authorService;

		public AuthorController(IAuthorService authorService)
		{
			_authorService = authorService;
		}

		[HttpGet]
		public  async Task<IActionResult> GetAuthors()
		{
			return Ok(await _authorService.GetAuthors());
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetAuthorById(int id)
		{
			return Ok(await _authorService.GetById(id));
		}

		[HttpPost]
		public async Task<IActionResult> AddAuthor(CreateAuthorDTO model)
		{
			await _authorService.Create(model);
			return Ok();
		}

		[HttpPut]
		public async Task<IActionResult> UpdateAuthor(UpdateAuthorDTO model)
		{
			await _authorService.Update(model);
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAuthor(int id)
		{
			await _authorService.Delete(id);
			return Ok();
		}
	}
}
