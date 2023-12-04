using HS12_BlogProject.Common.Models.DTOs;
using HS12_BlogProject.Application.Services.GenreService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HS12_BlogProject.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
	[ApiController]
	public class GenreController : ControllerBase
	{
		private readonly IGenreService _genreService;

		public GenreController(IGenreService genreService)
		{
			_genreService = genreService;
		}

		[HttpGet]
		public async Task<IActionResult> GetGenres()
		{
			return Ok(await _genreService.GetGenres());
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetGenreById(int id)
		{
			return Ok(await _genreService.GetById(id));
		}

		[HttpPost]
		public async Task<IActionResult> AddGenre(GenreDTO model)
		{
			await _genreService.Create(model);
			return Ok();
		}

		[HttpPut]
		public async Task<IActionResult> UpdateGenre(GenreDTO model)
		{
			await _genreService.Update(model);
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteGenre(int id)
		{
			await _genreService.Delete(id);
			return Ok();
		}
	}
}
