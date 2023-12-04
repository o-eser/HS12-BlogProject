using System.Diagnostics;
using HS12_BlogProject.Common.Models.DTOs;
using HS12_BlogProject.Presentation.APIService;
using HS12_BlogProject.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GenreDTO = HS12_BlogProject.Presentation.Models.GenreDTO;

namespace HS12_BlogProject.Presentation.Controllers
{
    [Authorize]
	public class GenreController : Controller
    {
        private readonly IApiService _apiService;

		public GenreController(IApiService apiService)
		{
			_apiService = apiService;
		}

		public async Task<IActionResult> Index()
        {
            var genres = await _apiService.GetAsync<List<GenreDTO>>("genre", HttpContext.Request.Cookies["access_token"]);
            return View(genres);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GenreDTO genreDTO)
        {
            if (ModelState.IsValid)
            {
                var genre = await _apiService.PostAsync<GenreDTO, GenreDTO>("genre", genreDTO, HttpContext.Request.Cookies["access_token"]);
                return RedirectToAction("Index");
            }
            return View(genreDTO);
        }

        
        public async Task<IActionResult> Edit(int id)
        {
            GenreDTO model = await _apiService.GetByIdAsync<GenreDTO>("genre", id, HttpContext.Request.Cookies["access_token"]);
			return View(model);
		}

        [HttpPost]
        public async Task<IActionResult> Edit(GenreDTO genreDTO)
        {
			if (ModelState.IsValid)
            {
				await _apiService.UpdateAsync<GenreDTO>("genre", genreDTO, HttpContext.Request.Cookies["access_token"]);
				return RedirectToAction("Index");
			}
			return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
			await _apiService.DeleteAsync<GenreDTO>("genre", id, HttpContext.Request.Cookies["access_token"]);
			return RedirectToAction("Index");
		}

    }
}