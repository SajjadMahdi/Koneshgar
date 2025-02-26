using TaskStatus = Koneshgar.Domain.Models.Tasks.TaskStatus;

namespace Koneshgar.Domain.Interfaces
{
    public interface ITaskStatusRepository:IGenericRepository<TaskStatus>
    {
        
    }
}
