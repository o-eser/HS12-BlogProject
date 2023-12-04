using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HS12_BlogProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HS12_BlogProject.Infrastructure.EntityTypeConfig
{
	internal class LikeConfig :BaseEntityConfig<Like>, IEntityTypeConfiguration<Like>
	{
		public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Like> builder)
		{
			builder.HasKey(x => x.Id);

			builder.HasOne(x => x.AppUser)
				.WithMany(x => x.Likes)
				.HasForeignKey(x => x.AppUserId)
				.OnDelete(DeleteBehavior.Restrict); 

			builder.HasOne(x => x.Post)
				.WithMany(x => x.Likes)
				.HasForeignKey(x => x.PostId)
				.OnDelete(DeleteBehavior.Restrict); 

			base.Configure(builder);
		}
	}
}
