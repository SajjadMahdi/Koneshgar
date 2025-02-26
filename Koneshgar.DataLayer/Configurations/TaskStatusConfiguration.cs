using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskStatus = Koneshgar.Domain.Models.Tasks.TaskStatus;

namespace Koneshgar.DataLayer.Configurations
{
    public class TaskStatusConfiguration : IEntityTypeConfiguration<TaskStatus>
    {
        public void Configure(EntityTypeBuilder<TaskStatus> builder)
        {
            builder.Property(x => x.Status).IsRequired();
            builder.HasData(new TaskStatus { Id = 1, Status = "Ongoing" }, new TaskStatus { Id = 2, Status = "Completed" });
        }
    }
}
