using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HS12_BlogProject.Domain.Enums;

namespace HS12_BlogProject.Common.Models.DTOs
{
	public class RegisterDTO
	{
		//Todo: DataAnnotations
		public string UserName { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public string Email { get; set; }
		public DateTime? CreateDate => DateTime.Now;
        public Status Status =>Status.Active;
    }
}
