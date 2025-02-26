using Koneshgar.Domain.Models.Base;
using Koneshgar.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koneshgar.Domain.Models.Tasks
{
    public class TaskEntity : BaseEntity<int>, IEntity
    {
        #region properties
        public string CreatorId { get; set; }
        public TimeOnly FromTime { get; set; }
        public TimeOnly ToTime { get; set; }
        public DateTime Deadline { get; set; }
        public List<TaskDays>? TasksDays { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TaskStatusId { get; set; }
        #endregion

        #region relations
        public TaskStatus TaskStatus { get; set; }
        public ICollection<UserTask> UserTasks { get; set; }
        public ICollection<Comment> Comments { get; set; }
        #endregion
    }
    public enum TaskDays
    {
        saturday,
        sunday,
        monday,
        tuesday,
        wednesday,
        thursday,
        friday
    }
}
