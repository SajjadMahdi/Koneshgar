using Koneshgar.Application.Handlers.Users.Validations;
using Core.Aspects.Autofac.Validation;
using Koneshgar.Application.Exceptions;
using Koneshgar.Application.Utilities.Responses.Abstract;
using Koneshgar.Application.Utilities.Responses.Concrete;
using Koneshgar.Application.Utilities.Statics;
using Koneshgar.Domain.Dtos.User;
using Koneshgar.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Koneshgar.Application.Services.Abstract;


namespace Koneshgar.Application.Handlers.Users.Commands
{
    public class LoginCommand:IRequest<IResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, IResponse>
        {
            private UserManager<User> _userManager;
            private ITokenService _tokenService;

            public LoginCommandHandler(UserManager<User> userManager,ITokenService tokenService)
            {
                _tokenService = tokenService;
                _userManager = userManager;
            }
            
            [ValidationAspect(typeof(LoginValidator))]
            public async Task<IResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(request.UserName);
                if (user == null)
                {
                    throw new ApiException(400, Messages.UserNameOrPasswordIsIncorrect);
                }
                if (!user.EmailConfirmed)
                {
                    throw new ApiException(400, Messages.ConfirmYourAccount);
                }
                var identityResult = await _userManager.CheckPasswordAsync(user, request.Password);
                if (!identityResult)
                {
                    throw new ApiException(400, Messages.UserNameOrPasswordIsIncorrect);
                }
                var token = await _tokenService.CreateToken(user);
                return new DataResponse<TokenDTO>(token, 200);
            }
        }
    }
}
