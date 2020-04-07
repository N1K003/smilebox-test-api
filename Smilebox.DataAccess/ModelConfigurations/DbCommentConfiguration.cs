using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smilebox.Data.Contracts.Models;

namespace Smilebox.Data.EntityFramework.ModelConfigurations
{
    public class DbCommentConfiguration : IEntityTypeConfiguration<DbComment>
    {
        public void Configure(EntityTypeBuilder<DbComment> builder)
        {
            builder.ToTable("Comments");

            builder.Property(x => x.Text).IsRequired();

            builder.HasOne(x => x.Post)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.PostId);
        }
    }
}