using HS12_BlogProject.Common.Models.DTOs;
using HS12_BlogProject.Common.Models.VMs;
using HS12_BlogProject.Presentation.APIService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HS12_BlogProject.Presentation.Controllers
{
    [Authorize]
    public class AuthorController : Controller
    {
        private readonly IApiService _apiService;

        public AuthorController(IApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: AuthorController
        public async Task<ActionResult> Index()
        {
            return View(await _apiService.GetAsync<List<AuthorVM>>("author", HttpContext.Request.Cookies["access_token"]));
        }

        // GET: AuthorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AuthorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAuthorDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                await _apiService.PostAsync<CreateAuthorDTO, AuthorVM>("author", model, HttpContext.Request.Cookies["access_token"]);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            UpdateAuthorDTO model =await _apiService.GetByIdAsync<UpdateAuthorDTO>("author", id, HttpContext.Request.Cookies["access_token"]);
            return View(model);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UpdateAuthorDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                await _apiService.UpdateAsync<UpdateAuthorDTO>("author", model, HttpContext.Request.Cookies["access_token"]);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        // GET: AuthorController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            await _apiService.DeleteAsync<AuthorVM>("author", id, HttpContext.Request.Cookies["access_token"]);
            return RedirectToAction(nameof(Index));
        }
    }
}
