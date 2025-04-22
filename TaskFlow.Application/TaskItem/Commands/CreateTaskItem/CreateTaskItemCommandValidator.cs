using FluentValidation;

namespace TaskFlow.Application.TaskItem.Commands.CreateTaskItem
{
    public class CreateTaskItemCommandValidator : AbstractValidator<CreateTaskItemCommand>
    {
        public CreateTaskItemCommandValidator()
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