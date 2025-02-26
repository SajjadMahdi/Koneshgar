using Koneshgar.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Koneshgar.DataLayer.Configurations
{
    public class UserTaskConfiguration : IEntityTypeConfiguration<UserTask>
    {
        public void Configure(EntityTypeBuilder<UserTask> builder)
        {
            builder.HasKey(ut => new { ut.TaskId, ut.UserId });
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.TaskId).IsRequired();
        }
    }
}
