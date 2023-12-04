using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HS12_BlogProject.Domain.Enums;

namespace HS12_BlogProject.Domain.Entities
{
    public class Genre : IBaseEntity
    {
        public Genre()
        {
            Posts = new HashSet<Post>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Status Status { get; set; }

        public ICollection<Post> Posts { get; set; }

    }
}
