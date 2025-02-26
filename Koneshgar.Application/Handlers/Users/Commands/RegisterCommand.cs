using AutoMapper;
using Koneshgar.Application.Handlers.Users.Validations;
using Core.Aspects.Autofac.Validation;
using Koneshgar.Application.Exceptions;
using Koneshgar.Application.Utilities.Responses.Abstract;
using Koneshgar.Application.Utilities.Responses.Concrete;
using Koneshgar.Application.Utilities.Statics;
using Koneshgar.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Koneshgar.Application.Utilities;


namespace Koneshgar.Application.Handlers.Users.Commands
{
    public class RegisterCommand:IRequest<IResponse>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }


        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, IResponse>
        {
            private UserManager<User> _userManager;
            private IMapper _mapper;
            private IEmailService _emailService;
            public RegisterCommandHandler(UserManager<User> userManager, IMapper mapper, IEmailService emailService)
            {
                _userManager = userManager;
                _mapper = mapper;
                _emailService = emailService;
            }

            [ValidationAspect(typeof(RegisterValidator))]
            public async Task<IResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                var email = await _userManager.FindByEmailAsync(request.Email);
                if (email != null)
                {
                    throw new ApiException(400, Messages.EmailIsAlreadyExist);
                }
                var username = await _userManager.FindByNameAsync(request.UserName);
                if (username != null)
                {
                    throw new ApiException(400, Messages.UsernameIsAlreadyExist);
                }
                if (request.Password != request.ConfirmPassword)
                {
                    throw new ApiException(400, Messages.PasswordDontMatchWithConfirmation);
                }
                var user = _mapper.Map<User>(request);
                var IdentityResult = await _userManager.CreateAsync(user, request.Password);
                if (IdentityResult.Succeeded)
                {
                    string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
                    var tokenEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
                    string link = "http://localhost:8080/confirmemail/" + user.Id + "/" + tokenEncoded;
                    #region for confarming with email
                    //await _emailService.ConfirmationMailAsync(link, request.Email);
                    //return new SuccessResponse(200, Messages.RegisterSuccessfully);
                    #endregion
                    return new SuccessResponse(200, link);

                }
                else
                {
                    throw new ApiException(400, IdentityResult.Errors.Select(e => e.Description).ToList());
                }
            }
        }
    }
}
