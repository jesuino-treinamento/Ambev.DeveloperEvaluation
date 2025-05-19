using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;

        public UpdateUserHandler(
            IUserRepository userRepository,
            IMapper mapper,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<UpdateUserResult> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateUserCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingUser = await _userRepository.GetByIdAsync(command.Id, cancellationToken);
            if (existingUser == null)
            {
                throw new DomainException($"User with ID {command.Id} not found for update");
            }

            var isEmailOrUsernameTaken = await _userRepository.ExistsWithEmailOrUsernameAsync(
                command.Email,
                command.Username,
                cancellationToken);

            if (!isEmailOrUsernameTaken)
            {
                throw new DomainException($"Email {command.Email} or username {command.Username} already in use by another user");
            }

            existingUser.Username = command.Username;
            existingUser.Email = command.Email;
            existingUser.Phone = command.Phone;
            existingUser.Role = command.Role;
            existingUser.Status = command.Status;
            existingUser.Role = command.Role;

            if (!string.IsNullOrEmpty(command.Password))
            {
                existingUser.Password = _passwordHasher.HashPassword(command.Password);
            }

            existingUser.UpdateName(command.Name.FirstName, command.Name.LastName);
            existingUser.UpdateAddress(
                command.Address.City,
                command.Address.Street,
                command.Address.Number,
                command.Address.ZipCode,
                command.Address.Geolocation.Lat,
                command.Address.Geolocation.Long);

            var updatedUser = await _userRepository.UpdateAsync(existingUser, cancellationToken);

            return _mapper.Map<UpdateUserResult>(updatedUser);
        }
    }
}
