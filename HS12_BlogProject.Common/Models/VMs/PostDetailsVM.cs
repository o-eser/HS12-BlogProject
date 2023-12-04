using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS12_BlogProject.Common.Models.VMs
{
	public class PostDetailsVM
	{
        public string Title	 { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }

        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string FullName => $"{AuthorFirstName} {AuthorLastName}";
        public string AuthorImagePath { get; set; }
    }
}
