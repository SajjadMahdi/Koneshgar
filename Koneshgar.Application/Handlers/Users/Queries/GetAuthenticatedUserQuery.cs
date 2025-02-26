using AutoMapper;
using Koneshgar.Application.Exceptions;
using Koneshgar.Application.Utilities.Responses.Abstract;
using Koneshgar.Application.Utilities.Responses.Concrete;
using Koneshgar.Application.Utilities.Statics;
using Koneshgar.Domain.Dtos.User;
using Koneshgar.Domain.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace Koneshgar.Application.Handlers.Users.Queries
{
    public class GetAuthenticatedUserQuery:IRequest<IResponse>
    {
        public string UserId { get; set; }
        public GetAuthenticatedUserQuery(string userid)
        {
            UserId = userid;
        }
        public class GetAuthenticatedUserQueryHander : IRequestHandler<GetAuthenticatedUserQuery, IResponse>
        {
            private UserManager<User> _userManager;
            private IMapper _mapper;

            public GetAuthenticatedUserQueryHander(UserManager<User> userManager,IMapper mapper)
            {
                _userManager = userManager;
                _mapper = mapper;
            }

            public async Task<IResponse> Handle(GetAuthenticatedUserQuery request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user == null)
                {
                    throw new ApiException(404, Messages.UserNotFound);
                }
                var mappeduser = _mapper.Map<UserDTO>(user);
                return new DataResponse<UserDTO>(mappeduser, 200);
            }
        }
    }
}
