using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HS12_BlogProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HS12_BlogProject.Infrastructure.EntityTypeConfig
{
	internal class GenreConfig: BaseEntityConfig<Genre>, IEntityTypeConfiguration<Genre>
	{
		public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Genre> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

			builder.HasMany(x=>x.Posts)
				.WithOne(x=>x.Genre)
				.HasForeignKey(x=>x.GenreId)
				.OnDelete(DeleteBehavior.Restrict);

			base.Configure(builder);
		}
	}
}
