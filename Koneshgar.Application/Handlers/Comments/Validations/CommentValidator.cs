using Koneshgar.Application.Handlers.Comments.Commands;
using FluentValidation;

namespace Koneshgar.Application.Handlers.Comments.Validations
{
    public class CreateCommentValidator: AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description").MinimumLength(5);
            RuleFor(x => x.TaskId).NotEmpty().WithMessage("TaskId is required");
        }
    }

    public class UpdateCommentValidator : AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description").MinimumLength(5);
        }
    }
}
