using Koneshgar.Domain.Dtos.User;
using Koneshgar.Domain.Models.Base;
using Koneshgar.Domain.Models.Tasks;

namespace Koneshgar.Domain.Dtos.Tasks
{
    public class TaskDetailDTO : BaseEntity<int>
    {
        #region properties
        public string CreatorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TaskStatusId { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string Deadline { get; set; }
        public List<TaskDays>? TasksDays { get; set; }

        #endregion
        #region reltions
        public List<UserTaskDTO> AssignedUsers { get; set; }
        public List<CommentDTO> Comments { get; set; }
        #endregion
    }
}
