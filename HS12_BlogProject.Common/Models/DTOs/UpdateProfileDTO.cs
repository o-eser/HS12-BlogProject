using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HS12_BlogProject.Common.Extensions;
using HS12_BlogProject.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace HS12_BlogProject.Common.Models.DTOs
{
	public class UpdateProfileDTO
	{
        //Todo: DataAnnotations
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
		public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public DateTime UpdateDate => DateTime.Now;
		public Status Status { get; set; }
        public string ImagePath { get; set; }


        [PictureFileExtension]
        public IFormFile UploadPath { get; set; }
    }
}
