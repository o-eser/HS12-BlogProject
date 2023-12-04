using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HS12_BlogProject.Common.Models.DTOs;
using HS12_BlogProject.Domain.Entities;
using HS12_BlogProject.Domain.Enums;
using HS12_BlogProject.Domain.Repositories;

namespace HS12_BlogProject.Application.Services.GenreService
{
	public class GenreService : IGenreService
	{
		private readonly IGenreRepository _genreRepository;
		private readonly IMapper _mapper;

		public GenreService(IGenreRepository genreRepository, IMapper mapper)
		{
			_genreRepository = genreRepository;
			_mapper = mapper;
		}

		public async Task Create(GenreDTO genreDTO)
		{
			//Genre genre = new Genre();

			//genre.Name = genreDTO.Name;

			Genre genre = _mapper.Map<Genre>(genreDTO);

			genre.Status = Status.Active;
			genre.UpdateDate = DateTime.Now;

			if (await _genreRepository.GetDefault(g => g.Name == genre.Name) != null)
			{
				throw new ArgumentException("Bu isimde bir kategori zaten var");
			}
			await _genreRepository.Create(genre);

		}

		public async Task<GenreDTO> Create()
		{

			return new GenreDTO();
		}

		public async Task<List<GenreDTO>> GetGenres()
		{
			ICollection<GenreDTO> genres = await _genreRepository.GetFilteredList(x => new GenreDTO
			{
				Name = x.Name,
				Id = x.Id
			}, g => g.Status != Status.Passive,
			   x => x.OrderBy(x => x.Name));

			return genres.ToList();
		}

		public async Task<GenreDTO> GetById(int id)
		{
			Genre genre = await _genreRepository.GetDefault(g => g.Id == id && g.Status != Status.Passive);

			var model = _mapper.Map<GenreDTO>(genre);

			return model;

			//return await _genreRepository.GetFilteredFirstOrDefault(x => new GenreDTO
			//{
			//	Name = x.Name,
			//}, g => g.Id == id && g.Status == Status.Active);
		}

		public async Task Update(GenreDTO genreDTO)
		{
			Genre genre = new Genre();


			genre = await _genreRepository.GetDefault(g => g.Id == genreDTO.Id);

			if (genre == null)
			{
				throw new ArgumentException("Böyle bir genre mevcut değil!");
			}

			genre.Name = genreDTO.Name;
			genre.UpdateDate = DateTime.Now;
			genre.Status = Status.Modified;

			await _genreRepository.Update(genre);

			//bool isGenreExist = await _genreRepository.Any(g => g.Id == genreDTO.Id);
			//if (isGenreExist)
			//{
			//	Genre genre = _mapper.Map<Genre>(genreDTO);
			//	await _genreRepository.Update(genre);
			//}
			//else
			//{
			//	throw new ArgumentException("Böyle bir genre mevcut değil!");
			//}

		}

		public async Task Delete(int id)
		{
			Genre genre = await _genreRepository.GetDefault(g => g.Id == id);

			if (id == 0)
			{
				throw new ArgumentException("Id 0 Olamaz!");

			}
			else if (genre == null)
			{
				throw new ArgumentException("Böyle bir genre mevcut değil!");
			}

			genre.Status = Status.Passive;
			genre.DeletedDate = DateTime.Now;

			await _genreRepository.Delete(genre);
		}
	}
}
