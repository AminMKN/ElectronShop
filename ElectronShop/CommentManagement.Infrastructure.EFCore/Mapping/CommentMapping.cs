using CommentManagement.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommentManagement.Infrastructure.EFCore.Mapping
{
    public class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Message).HasMaxLength(2000).IsRequired();
        }
    }
}
