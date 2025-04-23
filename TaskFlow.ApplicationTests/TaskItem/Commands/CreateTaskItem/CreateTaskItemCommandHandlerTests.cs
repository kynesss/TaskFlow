using Xunit;
using Moq;
using AutoMapper;
using TaskFlow.Domain.Interfaces;
using FluentAssertions;
using MediatR;

namespace TaskFlow.Application.TaskItem.Commands.CreateTaskItem.Tests
{
    public class CreateTaskItemCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_ShouldCallCreateAsync_WhenCommandIsValid()
        {
            // arrange

            var taskItemRepositoryMock = new Mock<ITaskItemRepository>();

            var command = new CreateTaskItemCommand()
            {
                Id = 1,
                Title = "Title",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid().ToString(),
                AssignedTo = Guid.NewGuid().ToString(),
                Description = "Description",
                Priority = Domain.Enums.TaskPriority.Low,
                Status = Domain.Enums.TaskStatus.Cancelled
            };

            var mapperMock = new Mock<IMapper>();
            mapperMock
                .Setup(x => x.Map<Domain.Entities.TaskItem>(command))
                .Returns(new Domain.Entities.TaskItem());

            var handler = new CreateTaskItemCommandHandler(taskItemRepositoryMock.Object, mapperMock.Object);

            // act

            var result = await handler.Handle(command, CancellationToken.None);

            // assert

            result.Should().Be(Unit.Value);
            taskItemRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<Domain.Entities.TaskItem>()), Times.Once);
        }
    }
}