using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HS12_BlogProject.Domain.Entities;
using HS12_BlogProject.Domain.Repositories;

namespace HS12_BlogProject.Infrastructure.Repositories
{
    public class AuthorRepository : BaseRepository<Author>,IAuthorRepository
    {
        public AuthorRepository(AppDbContext context) : base(context)
        {
        }
    }
}
