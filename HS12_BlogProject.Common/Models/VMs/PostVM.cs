using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HS12_BlogProject.Domain.Enums;

namespace HS12_BlogProject.Common.Models.VMs
{
	public class PostVM
	{
		public int Id { get; set; }
		//todo attribute
		[Required(ErrorMessage = "")]
		public string Title { get; set; }
		public string Content { get; set; }
        public string GenreName { get; set; }
		public string AuthorFirstName { get; set; }
		public string AuthorLastName { get; set; }
        public string ImagePath { get; set; }
		public string FullName => $"{AuthorFirstName} {AuthorLastName}";
        

		public int AuthorId { get; set; }
		public int GenreId { get; set; }
	}
}
