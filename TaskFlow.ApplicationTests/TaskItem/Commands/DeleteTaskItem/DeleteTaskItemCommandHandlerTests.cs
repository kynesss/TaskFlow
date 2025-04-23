using Xunit;
using Moq;
using TaskFlow.Domain.Interfaces;
using FluentAssertions;
using MediatR;

namespace TaskFlow.Application.TaskItem.Commands.DeleteTaskItem.Tests
{
    public class DeleteTaskItemCommandHandlerTests
    {
        [Fact()]
        public async Task HandleTest_ShouldCallDeleteAsync_WhenCommandIsValid()
        {
            // arrange

            var taskItemRepositoryMock = new Mock<ITaskItemRepository>();
            var command = new DeleteTaskItemCommand(1);

            var handler = new DeleteTaskItemCommandHandler(taskItemRepositoryMock.Object);

            // act

            var result = await handler.Handle(command, CancellationToken.None);

            // assert

            result.Should().Be(Unit.Value);
            taskItemRepositoryMock.Verify(x => x.DeleteAsync(command.Id), Times.Once);
        }
    }
}