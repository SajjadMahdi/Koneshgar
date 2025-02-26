using Koneshgar.Domain.Models.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Koneshgar.DataLayer.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.CommentDate).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.TaskId).IsRequired();
        }
    }
}
