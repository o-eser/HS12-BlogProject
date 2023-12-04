using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HS12_BlogProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HS12_BlogProject.Infrastructure.EntityTypeConfig
{
	internal class PostConfig : BaseEntityConfig<Post>, IEntityTypeConfiguration<Post>
	{
		public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Post> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
			builder.Property(x => x.Content).IsRequired().HasColumnType("ntext");

			builder.HasOne(x => x.Author)
				.WithMany(x => x.Posts)
				.HasForeignKey(x => x.AuthorId)
				.OnDelete(DeleteBehavior.Restrict);

			base.Configure(builder);
		}
	}
}
