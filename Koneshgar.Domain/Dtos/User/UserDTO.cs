using Koneshgar.Domain.Models.Base;

namespace Koneshgar.Domain.Dtos.User
{
    public class UserDTO : BaseEntity<string>
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
