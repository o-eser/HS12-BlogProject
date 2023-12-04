using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HS12_BlogProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HS12_BlogProject.Infrastructure.EntityTypeConfig
{
	internal class CommentConfig : BaseEntityConfig<Comment>, IEntityTypeConfiguration<Comment>
	{
		override public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Comment> builder)
		{
			builder.HasKey(x => x.Id);

			builder.HasOne(x => x.AppUser)
				.WithMany(x => x.Comments)
				.HasForeignKey(x => x.AppUserId)
				.OnDelete(DeleteBehavior.Restrict); // 1 user can have many comments but 1 comment can have only 1 user so we use HasForeignKey method to specify the foreign key and OnDelete method to specify the delete behavior  of the foreign key (Restrict means that if we delete the user, the comment will not be deleted)

			builder.HasOne(x => x.Post)
				.WithMany(x => x.Comments)
				.HasForeignKey(x => x.PostId)
				.OnDelete(DeleteBehavior.Restrict); // 1 post can have many comments but 1 comment can have only 1 post so we use HasForeignKey method to specify the foreign key and OnDelete method to specify the delete behavior  of the foreign key (Restrict means that if we delete the post, the comment will not be deleted)

			base.Configure(builder);
		}
	}
}
