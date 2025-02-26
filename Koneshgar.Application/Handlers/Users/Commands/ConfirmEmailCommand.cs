using Koneshgar.Application.Exceptions;
using Koneshgar.Application.Utilities.Responses.Abstract;
using Koneshgar.Application.Utilities.Responses.Concrete;
using Koneshgar.Application.Utilities.Statics;
using Koneshgar.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Koneshgar.Application.Handlers.Users.Commands
{
    public class ConfirmEmailCommand : IRequest<IResponse>
    {
        public string UserId { get; set; }
        public string Token { get; set; }


        public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, IResponse>
        {
            private UserManager<User> _userManager;

            public ConfirmEmailCommandHandler(UserManager<User> userManager)
            {
                _userManager = userManager;
            }

            public async Task<IResponse> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
            {
                if (request.UserId == null || request.Token == null)
                {
                    throw new ApiException(404, Messages.TokenOrUserNotFound);
                }
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user == null)
                {
                    throw new ApiException(404, Messages.UserNotFound);
                }
                if (user.EmailConfirmed)
                {
                    throw new ApiException(400, Messages.AlreadyAccountConfirmed);
                }
                var tokenDecodedBytes = WebEncoders.Base64UrlDecode(request.Token);
                var tokenDecoded = Encoding.UTF8.GetString(tokenDecodedBytes);
                var result = await _userManager.ConfirmEmailAsync(user, tokenDecoded);
                if (result.Succeeded)
                {
                    return new SuccessResponse(200, Messages.SuccessfullyAccountConfirmed);
                }
                throw new ApiException(400, Messages.AccountDontConfirmed);
            }
        }
    }

    
}
