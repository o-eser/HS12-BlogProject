using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HS12_BlogProject.Common.Models.DTOs;
using HS12_BlogProject.Common.Models.VMs;

namespace HS12_BlogProject.Application.Services.PostService
{
	public interface IPostService
	{
		Task Create(CreatePostDTO model);
		Task Update(UpdatePostDTO model);
		Task Delete(int id);
		Task<UpdatePostDTO> GetById(int id);
		Task<List<PostVM>> GetPosts();
		Task<CreatePostDTO> CreatePost();
		Task AddComment(CommentDTO model);
		Task AddLike(LikeDTO model);
		Task<PostDetailsVM> GetPostDetails(int id);
	}
}
