using Koneshgar.Domain.Dtos.User;
using Koneshgar.Domain.Models.Base;
using Koneshgar.Domain.Models.Tasks;

namespace Koneshgar.Domain.Dtos.Tasks
{
    public class TaskDTO : BaseEntity<int>
    {
        #region properties
        public string CreatorId { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string Deadline { get; set; }
        public List<TaskDays>? TasksDays { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public List<UserTaskDTO> AssignedUsers { get; set; }
        #endregion

    }
}
