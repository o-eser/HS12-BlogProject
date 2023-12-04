using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HS12_BlogProject.Common.Models.DTOs;
using HS12_BlogProject.Common.Models.VMs;
using HS12_BlogProject.Domain.Entities;
using HS12_BlogProject.Domain.Enums;
using HS12_BlogProject.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HS12_BlogProject.Application.Services.PostService
{
	public class PostService : IPostService
	{
		private readonly IPostRepository _postRepository;
		private readonly IAuthorRepository _authorRepository;
		private readonly IGenreRepository _genreRepository;
		private readonly ICommentRepository _commentRepository;
		private readonly IMapper _mapper;

		public PostService(IPostRepository postRepository, IAuthorRepository authorRepository, IGenreRepository genreRepository, ICommentRepository commentRepository, IMapper mapper)
		{
			_postRepository = postRepository;
			_authorRepository = authorRepository;
			_genreRepository = genreRepository;
			_commentRepository = commentRepository;
			_mapper = mapper;
		}

		public async Task Create(CreatePostDTO model)
		{
			//Post post = new Post()
			//{
			//	AuthorId = model.AuthorId,
			//	GenreId = model.GenreId,
			//	Title = model.Title,
			//	Content = model.Content,
			//};

			Post post = _mapper.Map<Post>(model);


			if (model.UploadPath != null)
			{
				//Sixlabors.ImageSharp
				Image image = Image.Load(model.UploadPath.OpenReadStream());

				image.Mutate(x => x.Resize(600, 560));

				Guid guid = Guid.NewGuid();
				image.Save($"wwwroot/images/{guid}.jpg");

				post.ImagePath = $"/images/{guid}.jpg";
			}
			else
				post.ImagePath = "/images/defaultpost.jpg";

			await _postRepository.Create(post);
		}


		
		public async Task<CreatePostDTO> CreatePost()
		{
			var authors = await _authorRepository.GetFilteredList(x => new AuthorVM
			{
				Id = x.Id,
				FirstName = x.FirstName,
				LastName = x.LastName
			}, x => x.Status != Status.Passive) as List<AuthorVM>;

			var genres = await _genreRepository.GetFilteredList(x => new GenreVM
			{
				Id = x.Id,
				Name = x.Name
			},
			x => x.Status != Status.Passive) as List<GenreVM>;

			CreatePostDTO createPostDTO = new CreatePostDTO()
			{
				Authors = authors,
				Genres = genres
			};

			return createPostDTO;
		}

		public async Task<List<PostVM>> GetPosts()
		{
			ICollection<PostVM> posts = await _postRepository.GetFilteredList(x => new PostVM
			{
				AuthorFirstName = x.Author.FirstName,
				AuthorLastName = x.Author.LastName,
				//Content = x.Content,
				Title = x.Title,
				GenreName = x.Genre.Name,
				Id = x.Id,
				//ImagePath = x.ImagePath,

			}, g => g.Status != Status.Passive,
			x => x.OrderByDescending(x => x.CreatedDate),
			  x => x.Include(x => x.Genre)
			  .Include(x => x.Author));

			return posts.ToList();
		}
		
		public async Task<UpdatePostDTO> GetById(int id)
		{
			Post post = await _postRepository.GetDefault(g => g.Id == id);

			var model = _mapper.Map<UpdatePostDTO>(post);

			model.Authors = await _authorRepository.GetFilteredList(x => new AuthorVM
			{
				Id = x.Id,
				FirstName = x.FirstName,
				LastName = x.LastName
			}, x => x.Status != Status.Passive,
			x=>x.OrderBy(x=>x.FirstName)) as List<AuthorVM>;

			model.Genres = await _genreRepository.GetFilteredList(x => new GenreVM
			{
				Id = x.Id,
				Name = x.Name
			},x=>x.Status!=Status.Passive,x=>x.OrderBy(x=>x.Name)) as List<GenreVM>;

			return model;

			#region Mapping öncesi
			//var authors = await _authorRepository.GetFilteredList(x => new AuthorVM
			//{
			//	Id = x.Id,
			//	FirstName = x.FirstName,
			//	LastName = x.LastName
			//}, x => x.Status != Status.Passive) as List<AuthorVM>;

			//var genres = await _genreRepository.GetFilteredList(x => new GenreVM
			//{
			//	Id = x.Id,
			//	Name = x.Name
			//},
			//x => x.Status != Status.Passive) as List<GenreVM>;

			//return await _postRepository.GetFilteredFirstOrDefault(x => new UpdatePostDTO
			//{
			//	Content = x.Content,
			//	Title = x.Title,
			//	ImagePath = x.ImagePath,
			//	AuthorId = x.AuthorId,
			//	GenreId = x.GenreId,
			//	Authors = authors,
			//	Genres = genres,
			//}, g => g.Id == id && g.Status != Status.Passive);

			#endregion
		}


		public async Task Update(UpdatePostDTO model)
		{
			Post post = new Post()
			{
				AuthorId = model.AuthorId,
				GenreId = model.GenreId,
				Content = model.Content,
				Title = model.Title,
				ImagePath = model.ImagePath,
				UpdateDate = DateTime.Now, //Todo: veritabanına kaydedilirken eklenmeli
			};



			if (model.UploadPath != null)
			{
				//Sixlabors.ImageSharp
				Image image = Image.Load(model.UploadPath.OpenReadStream());

				image.Mutate(x => x.Resize(600, 560));

				Guid guid = Guid.NewGuid();
				image.Save($"wwwroot/images/{guid}.jpg");

				post.ImagePath = $"/images/{guid}.jpg";
			}
			else
				post.ImagePath = "/images/defaultpost.jpg";

			await _postRepository.Update(post);
		}

		public async Task Delete(int id)
		{
			Post post = await _postRepository.GetDefault(g => g.Id == id);

			if (id == 0)
			{
				throw new ArgumentException("Id 0 Olamaz!");

			}
			else if (post == null)
			{
				throw new ArgumentException("Böyle bir post mevcut değil!");
			}

			post.Status = Status.Passive;
			post.DeletedDate = DateTime.Now;

			await _postRepository.Delete(post);
		}
		//todo: yeri değişecek
		public async Task AddComment(CommentDTO model)
		{
			Comment comment = new Comment()
			{
				Title = model.Title,
				Content = model.Content,
				AppUserId = model.AppUserId,
				PostId = model.PostId
			};

			await _commentRepository.Create(comment);
		}
		//todo: yeri değişecek
		public async Task AddLike(LikeDTO model)
		{
			Comment comment = new Comment()
			{
				AppUserId = model.AppUserId,
				PostId = model.PostId,
			};

			await _commentRepository.Create(comment);
		}

		public async Task<PostDetailsVM> GetPostDetails(int id)
		{
			var post = await _postRepository.GetFilteredFirstOrDefault(x => new PostDetailsVM
			{
				Title = x.Title,
				Content = x.Content,
				ImagePath = x.ImagePath,
				CreatedDate = x.CreatedDate,
				AuthorFirstName = x.Author.FirstName,
				AuthorLastName = x.Author.LastName,
				AuthorImagePath = x.Author.ImagePath
			}, g => g.Id == id && g.Status != Status.Passive, include: x => x.Include(x => x.Author));

			return post;
		}
	}
}


