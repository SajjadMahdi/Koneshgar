using Koneshgar.Domain.Dtos.User;
using Koneshgar.Domain.Models.Users;

namespace Koneshgar.Application.Services.Abstract
{
    public interface ITokenService
    {
        Task<TokenDTO> CreateToken (User user);
    }
}
