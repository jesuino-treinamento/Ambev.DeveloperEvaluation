
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common.Pagination;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUser
{
    public class GetAllUsersQuery : IRequest<PaginatedList<GetAllUserResult>>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
        public string Order { get; set; } = "username, email";

        public ValidationResultDetail Validate()
        {
            return new ValidationResultDetail(new GetAllUsersQueryValidator().Validate(this));
        }
    }
}
