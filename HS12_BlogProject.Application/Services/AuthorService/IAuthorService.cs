using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HS12_BlogProject.Common.Models.DTOs;
using HS12_BlogProject.Common.Models.VMs;

namespace HS12_BlogProject.Application.Services.AuthorService
{
	public interface IAuthorService
	{
		Task Create(CreateAuthorDTO model);
		Task Update(UpdateAuthorDTO model);
		Task Delete(int id);
		Task<UpdateAuthorDTO> GetById(int id);
		Task<List<AuthorVM>> GetAuthors();
		Task<CreateAuthorDTO> CreateAuthor();
	}
}
