﻿using Koneshgar.Application.Exceptions;
using Koneshgar.Application.Utilities.Responses.Abstract;
using Koneshgar.Application.Utilities.Responses.Concrete;
using Koneshgar.Application.Utilities.Statics;
using Koneshgar.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Koneshgar.Application.Handlers.Comments.Commands
{
    public class RemoveCommentCommand:IRequest<IResponse>
    {
        public int Id { get; set; }
        public RemoveCommentCommand(int id)
        {
            Id = id;
        }

        public class RemoveCommentCommandHandler : IRequestHandler<RemoveCommentCommand, IResponse>
        {
            private ICommentRepository _commentRepository;
            private IUnitOfWork _unitOfWork;
            private IHttpContextAccessor _httpContextAccessor;

            public RemoveCommentCommandHandler(ICommentRepository commentRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            {
                _commentRepository = commentRepository;
                _unitOfWork = unitOfWork;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<IResponse> Handle(RemoveCommentCommand request, CancellationToken cancellationToken)
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
                _commentRepository.Remove(existcomment);
                await _unitOfWork.SaveChangesAsync();
                return new SuccessResponse(200, Messages.DeletedSuccessfully);
            }
        }
    }
}
