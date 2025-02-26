using Koneshgar.Domain.Models.Base;
using Koneshgar.Domain.Models.Users;

namespace Koneshgar.Domain.Models.Tasks
{
    public class Comment : BaseEntity<int>,IEntity
    {
        #region properties
        public DateTime CommentDate { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public int TaskId { get; set; }
        #endregion
        #region relations
        public User User { get; set; }
        public TaskEntity Task { get; set; }
        #endregion
    }
}
