using Koneshgar.Domain.Dtos.Tasks;
using Koneshgar.Domain.Models.Base;

namespace Koneshgar.Domain.Dtos.User
{
    public class UserTaskDTO : BaseEntity<int>
    {
        #region properties
        public int TaskId { get; set; }
        public string UserId { get; set; }
        #endregion

        #region realtions
        public TaskDTO Task { get; set; }
        public UserDTO User { get; set; }
        #endregion
    }
}
