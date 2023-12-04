using HS12_BlogProject.Common.Models.DTOs;
using HS12_BlogProject.Application.Services.PostService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HS12_BlogProject.API.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class PostController : ControllerBase
	{
		private readonly IPostService _postService;

		public PostController(IPostService postService)
		{
			_postService = postService;
		}

		[HttpGet]
		public async Task<IActionResult> GetPosts()
		{
			return Ok(await _postService.GetPosts());
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetPostById(int id)
		{
			return Ok(await _postService.GetById(id));
		}

		[HttpGet]
		[Route("CreatePost")]
		public async Task<IActionResult> CreatePost()
		{
            
            return Ok(await _postService.CreatePost());
        }

		[HttpPost]
		public async Task<IActionResult> AddPost(CreatePostDTO model)
		{
			await _postService.Create(model);
			return Ok();
		}

		[HttpPut]
		public async Task<IActionResult> UpdatePost(UpdatePostDTO model)
		{
			await _postService.Update(model);
			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> DeletePost(int id)
		{
			await _postService.Delete(id);
			return Ok();
		}
	}
}
