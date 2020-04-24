using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resources.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resources.Data.Mapping
{
    public class ResourcesMapping : IEntityTypeConfiguration<Resources<string>>
    {

        public void Configure(EntityTypeBuilder<Resources<string>> builder)
        {

            builder.HasKey(x => x.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();
        }

    }
}
