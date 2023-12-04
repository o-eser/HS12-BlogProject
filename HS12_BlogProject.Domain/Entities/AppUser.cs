using HS12_BlogProject.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace HS12_BlogProject.Domain.Entities
{
    public class AppUser : IdentityUser, IBaseEntity
    {
        public AppUser()
        {
            Comments = new HashSet<Comment>();
            Likes = new HashSet<Like>();
        }
        public string ImagePath { get; set; }

        [NotMapped]
        public IFormFile UploadPath { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Status Status { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}
