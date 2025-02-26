using AutoMapper;
using Koneshgar.Application.Handlers.Comments.Validations;
using Core.Aspects.Autofac.Validation;
using Koneshgar.Application.Exceptions;
using Koneshgar.Application.Utilities.Responses.Abstract;
using Koneshgar.Application.Utilities.Responses.Concrete;
using Koneshgar.Application.Utilities.Statics;
using Koneshgar.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Koneshgar.Application.Handlers.Comments.Commands
{
    public class UpdateCommentCommand:IRequest<IResponse>
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, IResponse>
        {
            private ICommentRepository _commentRepository;
            private IMapper _mapper;
            private IHttpContextAccessor _httpContextAccessor;
            private IUnitOfWork _unitOfWork;

            public UpdateCommentCommandHandler(ICommentRepository commentRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
            {
                _commentRepository = commentRepository;
                _mapper = mapper;
                _httpContextAccessor = httpContextAccessor;
                _unitOfWork = unitOfWork;
            }

            [ValidationAspect(typeof(UpdateCommentValidator))]
            public async Task<IResponse> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
            {
                var userid = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                var existcomment = await _commentRepository.GetByIdAsync(request.Id);
                if (existcomment == null)
                {
                    throw new ApiException(404, Messages.NotFound);
                }
                if (existcomment.UserId != userid)
                {
                    throw new ApiException(401, Messages.CommentNotYours);
                }
                var updatedcomment = _mapper.Map(request, existcomment);
                _commentRepository.Update(updatedcomment);
                await _unitOfWork.SaveChangesAsync();
                return new SuccessResponse(200,Messages.UpdatedSuccessfully);
            }
        }
    }
}
