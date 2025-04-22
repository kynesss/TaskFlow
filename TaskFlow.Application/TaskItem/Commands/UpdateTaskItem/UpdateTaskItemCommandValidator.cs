using FluentValidation;

namespace TaskFlow.Application.TaskItem.Commands.EditTaskItem
{
    public class UpdateTaskItemCommandValidator : AbstractValidator<UpdateTaskItemCommand>
    {
        public UpdateTaskItemCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100);

            RuleFor(x => x.Priority)
                .IsInEnum();

            RuleFor(x => x.Status)
                .IsInEnum();
        }
    }
}