using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser
{
    public class GetUserCommand : IRequest<GetUserResult>
    {
        public Guid Id { get; }

        public GetUserCommand(Guid id)
        {
            Id = id;
        }
    }
}
