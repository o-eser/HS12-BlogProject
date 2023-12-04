using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HS12_BlogProject.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace HS12_BlogProject.Domain.Entities
{
    public class Author : IBaseEntity
    {
        public Author()
        {
            Posts = new HashSet<Post>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }

		[NotMapped]
		public IFormFile UploadPath { get; set; }

		public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Status Status { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
