using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HS12_BlogProject.Common.Extensions;
using HS12_BlogProject.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace HS12_BlogProject.Common.Models.DTOs
{
	public class CreateAuthorDTO
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		
		public string? ImagePath { get; set; }
		[PictureFileExtension]
		public IFormFile? UploadPath { get; set; }


        //Todo : Diğer DTO'lar için de aynı işlemleri yapınız.
        [Required(AllowEmptyStrings = true)]
        public DateTime CreateDate => DateTime.Now;
        [Required(AllowEmptyStrings = true)]
        public Status Status => Status.Active;
    }
}
