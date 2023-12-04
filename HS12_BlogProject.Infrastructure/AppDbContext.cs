using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HS12_BlogProject.Domain.Entities;
using HS12_BlogProject.Infrastructure.EntityTypeConfig;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HS12_BlogProject.Infrastructure
{
	public class AppDbContext : IdentityDbContext<AppUser>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<AppUser> Users { get; set; }
		public DbSet<Author> Authors { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Like> Likes { get; set; }
		public DbSet<Post> Posts { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration(new AppUserConfig());
			builder.ApplyConfiguration(new AuthorConfig());
			builder.ApplyConfiguration(new CommentConfig());
			builder.ApplyConfiguration(new GenreConfig());
			builder.ApplyConfiguration(new LikeConfig());
			builder.ApplyConfiguration(new PostConfig());



			base.OnModelCreating(builder);
		}
	}
}
