using Koneshgar.DataLayer.Contexts;
using Koneshgar.Domain.Interfaces;
using TaskStatus = Koneshgar.Domain.Models.Tasks.TaskStatus;

namespace Koneshgar.DataLayer.Repositories
{
    public class TaskStatusRepository : EfEntityRepositoryBase<TaskStatus, TaskContext>, ITaskStatusRepository
    {
        public TaskStatusRepository(TaskContext context) : base(context)
        {

        }
    }
}
