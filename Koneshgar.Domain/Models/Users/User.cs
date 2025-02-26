using Koneshgar.Domain.Models.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Koneshgar.Domain.Models.Users
{
    public class User : IdentityUser
    {
        #region properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        #endregion

        #region relations
        public ICollection<TaskEntity> Tasks { get; set; }
        public ICollection<Comment> Comments { get; set; }
        #endregion
    }
}
