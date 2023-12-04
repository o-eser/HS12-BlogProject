using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HS12_BlogProject.Common.Extensions;
using HS12_BlogProject.Common.Models.VMs;
using HS12_BlogProject.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace HS12_BlogProject.Common.Models.DTOs
{
	public class UpdatePostDTO
	{
		public int Id { get; set; }
		//todo attribute
		[Required(ErrorMessage = "")]
		public string Title { get; set; }
		public string Content { get; set; }
		public string ImagePath { get; set; }
		[PictureFileExtension]
		public IFormFile UploadPath { get; set; }

		public DateTime UpdateDate => DateTime.Now;
		public Status Status => Status.Modified;
		public int AuthorId { get; set; }
		public int GenreId { get; set; }

		public List<GenreVM> Genres { get; set; }
		public List<AuthorVM> Authors { get; set; }
	}
}
