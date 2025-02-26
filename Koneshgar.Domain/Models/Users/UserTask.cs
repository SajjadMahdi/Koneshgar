using Koneshgar.Domain.Models.Base;
using Koneshgar.Domain.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koneshgar.Domain.Models.Users
{
    public class UserTask : BaseEntity<int>, IEntity
    {
        #region properties
        public int TaskId { get; set; }
        public string UserId { get; set; }
        #endregion

        #region realtions
        public TaskEntity Task { get; set; }
        public User User { get; set; }
        #endregion
    }
}
