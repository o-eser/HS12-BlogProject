using HS12_BlogProject.Common.Models.DTOs;
using HS12_BlogProject.Common.Models.VMs;
using HS12_BlogProject.Presentation.APIService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HS12_BlogProject.Presentation.Controllers
{
	[Authorize]
	public class PostController : Controller
    {
        private readonly IApiService _apiService;

        public PostController(IApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: PostController
        public async Task<ActionResult> Index()
        {
            return View(await _apiService.GetAsync<List<PostVM>>("post", HttpContext.Request.Cookies["access_token"]));
        }

        // GET: PostController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PostController/Create
        public async Task<ActionResult> Create()
        {
            var model = await _apiService.GetCreateModelAsync<CreatePostDTO>("post/CreatePost", HttpContext.Request.Cookies["access_token"]);
            ViewBag.Genres = new SelectList(model.Genres, "Id", "Name");
            ViewBag.Authors = new SelectList(model.Authors, "Id", "FirstName","LastName");

			return View();
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreatePostDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                await _apiService.PostAsync<CreatePostDTO, CreatePostDTO>("post", model, HttpContext.Request.Cookies["access_token"]);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            return View(await _apiService.GetByIdAsync<UpdatePostDTO>("post", id, HttpContext.Request.Cookies["access_token"]));
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UpdatePostDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                _apiService.UpdateAsync<UpdatePostDTO>("post", model, HttpContext.Request.Cookies["access_token"]);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            await _apiService.DeleteAsync<PostVM>("post", id, HttpContext.Request.Cookies["access_token"]);
            return RedirectToAction(nameof(Index));
        }


    }
}
