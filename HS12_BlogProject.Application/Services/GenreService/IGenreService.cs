using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HS12_BlogProject.Common.Models.DTOs; 

namespace HS12_BlogProject.Application.Services.GenreService
{
	public interface IGenreService
	{
		Task<GenreDTO> GetById(int id);
		Task<List<GenreDTO>> GetGenres();
		Task Create(GenreDTO genreDTO);
		Task<GenreDTO> Create();
		Task Update(GenreDTO genreDTO);
		Task Delete(int id);
	}
}
