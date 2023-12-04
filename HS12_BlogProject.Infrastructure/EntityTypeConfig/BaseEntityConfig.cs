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
	internal class BaseEntityConfig<T> : IEntityTypeConfiguration<T> where T : class, IBaseEntity
	{
		public virtual void Configure(EntityTypeBuilder<T> builder)
		{
			builder.Property(x => x.CreatedDate).IsRequired(true);
			builder.Property(x => x.DeletedDate).IsRequired(false);
			builder.Property(x => x.UpdateDate).IsRequired(false);
			builder.Property(x => x.Status).IsRequired(true);

		}
	}
}
