using MediatR;

namespace TaskFlow.Application.User.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public string Id { get; }

        public GetUserByIdQuery(string id)
        {
            Id = id;
        }
    }
}