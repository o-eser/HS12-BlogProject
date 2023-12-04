using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HS12_BlogProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HS12_BlogProject.Infrastructure.EntityTypeConfig
{
	internal class AppUserConfig :BaseEntityConfig<AppUser> , IEntityTypeConfiguration<AppUser>
	{
		public override void Configure(EntityTypeBuilder<AppUser> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x=>x.UserName).IsRequired().HasMaxLength(30);
			builder.Property(x => x.ImagePath).IsRequired(false);

			base.Configure(builder);
		}

	}
}
