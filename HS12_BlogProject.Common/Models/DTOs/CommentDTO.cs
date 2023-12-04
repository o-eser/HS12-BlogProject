using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HS12_BlogProject.Domain.Entities;
using HS12_BlogProject.Domain.Enums;

namespace HS12_BlogProject.Common.Models.DTOs
{
	public class CommentDTO
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }

		public string AppUserId { get; set; }
		public AppUser AppUser { get; set; }
		public int PostId { get; set; }
		public Post Post { get; set; }
	}
}
