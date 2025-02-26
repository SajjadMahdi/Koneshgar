using Koneshgar.Domain.Models.Base;

namespace Koneshgar.Domain.Dtos.Tasks
{
    public class TaskStatusDTO : BaseEntity<int>
    {
        #region properties
        public string Status { get; set; }
        #endregion
    }
}
