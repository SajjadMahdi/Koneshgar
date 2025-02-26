using Koneshgar.Domain.Dtos.User;
using Koneshgar.Domain.Models.Base;

namespace Koneshgar.Domain.Dtos.Tasks
{
    public class CommentDTO : BaseEntity<int>
    {
        #region properties
        public DateTime CommentDate { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public int TaskId { get; set; }

        #endregion
        #region relations
        public UserDTO User { get; set; }
        public TaskDTO Task { get; set; }
        #endregion
    }
}
