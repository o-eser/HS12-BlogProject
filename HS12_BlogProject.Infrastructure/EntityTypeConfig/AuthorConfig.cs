using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HS12_BlogProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HS12_BlogProject.Infrastructure.EntityTypeConfig
{
	internal class AuthorConfig : BaseEntityConfig<Author>,IEntityTypeConfiguration<Author>
	{
		public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Author> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.FirstName).IsRequired().HasMaxLength(30);
			builder.Property(x => x.LastName).IsRequired().HasMaxLength(30);
			builder.Property(x => x.ImagePath).IsRequired(false);

			base.Configure(builder);
		}

	}
}
