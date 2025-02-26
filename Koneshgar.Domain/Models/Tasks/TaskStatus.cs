using Koneshgar.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koneshgar.Domain.Models.Tasks
{
    public class TaskStatus : BaseEntity<int>, IEntity
    {
        #region properties
        public string Status { get; set; }
        #endregion
        #region relations
        public ICollection<TaskEntity> Tasks { get; set; }
        #endregion
    }
}
