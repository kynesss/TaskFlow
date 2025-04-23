using Xunit;
using FluentValidation.TestHelper;

namespace TaskFlow.Application.TaskItem.Commands.CreateTaskItem.Tests
{
    public class CreateTaskItemCommandValidatorTests
    {
        [Fact()]
        public void Validate_ShouldHaveError_WhenInvalidCommand()
        {
            // arrange

            var validator = new CreateTaskItemCommandValidator();
            var command = new CreateTaskItemCommand();

            // act

            var result = validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact()]
        public void Validate_ShouldNotHaveError_WhenValidCommand()
        {
            // arrange

            var validator = new CreateTaskItemCommandValidator();
            var command = new CreateTaskItemCommand()
            {
                Title = "Title",
                Status = Domain.Enums.TaskStatus.Cancelled,
                Priority = Domain.Enums.TaskPriority.Critical
            };

            // act

            var result = validator.TestValidate(command);

            // assert

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void Validate_ShouldHaveError_WhenTitleTooLong()
        {
            // arrange

            var validator = new CreateTaskItemCommandValidator();
            var command = new CreateTaskItemCommand();
            command.Title = new string('a', 101);

            // act

            var result = validator.TestValidate(command);

            // assert

            result.ShouldHaveValidationErrorFor(x => x.Title);
        }
    }
}