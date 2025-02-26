﻿using AutoMapper;
using Koneshgar.Application.Handlers.Comments.Validations;
using Core.Aspects.Autofac.Validation;
using Koneshgar.Application.Utilities.Responses.Abstract;
using Koneshgar.Application.Utilities.Responses.Concrete;
using Koneshgar.Application.Utilities.Statics;
using Koneshgar.Domain.Interfaces;
using Koneshgar.Domain.Models.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Koneshgar.Application.Handlers.Comments.Commands
{
    public class CreateCommentCommand:IRequest<IResponse>
    {
        public int TaskId { get; set; }
        public string Description { get; set; }

        public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, IResponse>
        {
            private ICommentRepository _commentRepository;
            private IMapper _mapper;
            private IHttpContextAccessor _httpContextAccessor;
            private IUnitOfWork _unitOfWork;

            public CreateCommentCommandHandler(ICommentRepository commentRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
            {
                _commentRepository = commentRepository;
                _mapper = mapper;
                _httpContextAccessor = httpContextAccessor;
                _unitOfWork = unitOfWork;
            }

            
            [ValidationAspect(typeof(CreateCommentValidator))]
            public async Task<IResponse> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
            {
                var userid = _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                var comment = _mapper.Map<Comment>(request);
                comment.CommentDate = DateTime.UtcNow;
                comment.UserId = userid;
                await _commentRepository.AddAsync(comment);
                await _unitOfWork.SaveChangesAsync();
                return new SuccessResponse(200, Messages.AddedSuccesfully);
            }
        }



    }
}
