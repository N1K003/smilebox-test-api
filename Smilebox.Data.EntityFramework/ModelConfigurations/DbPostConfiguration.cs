using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smilebox.Data.Contracts.Models;

namespace Smilebox.Data.EntityFramework.ModelConfigurations
{
    public class DbPostConfiguration : IEntityTypeConfiguration<DbPost>
    {
        public void Configure(EntityTypeBuilder<DbPost> builder)
        {
            builder.ToTable("Posts");

            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Text).IsRequired();
        }
    }
}