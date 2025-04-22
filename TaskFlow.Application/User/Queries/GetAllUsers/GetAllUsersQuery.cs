using MediatR;

namespace TaskFlow.Application.User.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
    {
    }
}