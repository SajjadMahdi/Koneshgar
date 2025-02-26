using Koneshgar.DataLayer.Contexts;
using Koneshgar.Domain.Dtos.Tasks;
using Koneshgar.Domain.Dtos.User;
using Koneshgar.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task = Koneshgar.Domain.Models.Tasks.TaskEntity;

namespace Koneshgar.DataLayer.Repositories
{
    public class TaskRepository : EfEntityRepositoryBase<Task, TaskContext>, ITaskRepository
    {
        public TaskRepository(TaskContext context) : base(context)
        {

        }

        public async Task<TaskDetailDTO> GetTaskDetailByIdAsync(int id)
        {
            return await _context.Tasks.Select(task => new TaskDetailDTO
            {
                Id = task.Id,
                Title = task.Title,
                FromTime = task.FromTime.ToString("HH:mm", null),
                ToTime = task.ToTime.ToString("HH:mm", null),
                Deadline = task.Deadline.ToString("dd-MM-yyyy HH:mm", null),
                TasksDays = task.TasksDays,
                Description = task.Description,
                CreatorId = task.CreatorId,
                TaskStatusId = task.TaskStatusId,
                AssignedUsers = task.UserTasks.Select(usertask => new UserTaskDTO
                {
                    TaskId = usertask.TaskId,
                    UserId = usertask.UserId,
                    User = new UserDTO
                    {
                        FirstName = usertask.User.FirstName,
                        LastName = usertask.User.LastName,
                        Email = usertask.User.Email,
                        Id = usertask.User.Id,
                        UserName = usertask.User.UserName
                    }
                }).ToList(),
                Comments = task.Comments.Select(comment => new CommentDTO
                {
                    Id = comment.Id,
                    Description = comment.Description,
                    UserId = comment.UserId,
                    TaskId = comment.TaskId,
                    CommentDate = comment.CommentDate,
                    User = new UserDTO
                    {
                        FirstName = comment.User.FirstName,
                        LastName = comment.User.LastName,
                        Email = comment.User.Email,
                        Id = comment.User.Id,
                        UserName = comment.User.UserName
                    }
                }).ToList()
            }).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<TaskDTO>> GetTasksByUserIdAsync(string userid)
        {
            return await _context.Tasks.Where(ut => ut.UserTasks.Any(u => u.UserId == userid) || ut.CreatorId == userid).Select(task => new TaskDTO
            {
                Id = task.Id,
                Title = task.Title,
                FromTime = task.FromTime.ToString("HH:mm", null),
                ToTime = task.ToTime.ToString("HH:mm", null),
                Deadline = task.Deadline.ToString("dd-MM-yyyy HH:mm", null),
                TasksDays = task.TasksDays,
                CreatorId = task.CreatorId,
                Status = task.TaskStatus.Status,
                AssignedUsers = task.UserTasks.Select(usertask => new UserTaskDTO
                {
                    TaskId = usertask.TaskId,
                    UserId = usertask.UserId,
                    User = new UserDTO
                    {
                        FirstName = usertask.User.FirstName,
                        LastName = usertask.User.LastName,
                        Email = usertask.User.Email,
                        Id = usertask.User.Id,
                        UserName = usertask.User.UserName
                    }
                }).ToList(),
            }).ToListAsync();
        }

        public async Task<Task> GetTaskWithUserTasksByIdAsync(int id)
        {
            return await _context.Tasks.Include(x => x.UserTasks).FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
