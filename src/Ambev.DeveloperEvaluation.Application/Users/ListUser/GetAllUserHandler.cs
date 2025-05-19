using Ambev.DeveloperEvaluation.Domain.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUser
{
    public class GetAllUserHandler : IRequestHandler<GetAllUsersQuery, PaginatedList<GetAllUserResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUserHandler(IUserRepository userRepository, IMapper mapper)
            => (_userRepository, _mapper) = (userRepository, mapper);

        public async Task<PaginatedList<GetAllUserResult>> Handle(
            GetAllUsersQuery request, 
            CancellationToken cancellationToken)
        {         
            var users = await _userRepository.GetAllPaginatedAsync(
                request.Page, 
                request.Size, 
                request.Order);
            
            return new(
                _mapper.Map<List<GetAllUserResult>>(users.Items),
                users.TotalCount,
                users.PageNumber,
                users.PageSize);
        }
    }
}