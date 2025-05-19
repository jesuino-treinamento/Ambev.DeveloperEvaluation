using Ambev.DeveloperEvaluation.Domain.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DefaultContext _context;
        public UserRepository(DefaultContext context)
        {
            _context = context;
        }
        public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
        {
            var exists = await _context.Users
                .AnyAsync(u => u.Email == user.Email || u.Username == user.Username, cancellationToken);

            if (exists)
                throw new DomainException("A user with the same email or username already exists");
            
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }
        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Users.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }
        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await GetByIdAsync(id, cancellationToken);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == user.Id, cancellationToken);

            if (existingUser == null)
            {
                throw new DomainException($"User with ID {user.Id} not found for update");
            }

            _context.Entry(existingUser).CurrentValues.SetValues(user);
            existingUser.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return existingUser;
        }

        public async Task<bool> ExistsWithEmailOrUsernameAsync(string email, string username, CancellationToken cancellationToken = default)
        {
            return await _context.Users
                    .AsNoTracking()
                    .AnyAsync(u => u.Email == email || u.Username == username);
        }

        public async Task<(IEnumerable<User> Users, int totalCount)> GetAllAsync(int page, int size, string orderBy)
        {
            var query = _context.Users
             .Include(s => s.Customer)
                 .ThenInclude(c => c.User)
             .AsQueryable();

            query = orderBy.ToLower() switch
            {
                "saledate" => query.OrderByDescending(s => s.Customer),
                "total_amount" => query.OrderByDescending(s => s.Id),
                "status" => query.OrderBy(s => s.Status),
                _ => query.OrderByDescending(s => s.Customer)
            };

            var totalCount = await query.CountAsync();

            var users = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return (users, totalCount);
        }
        public async Task<PaginatedList<User>> GetAllPaginatedAsync(int page, int size, string orderBy)
        {
            if (page < 1) throw new ArgumentException("Page must be greater than 0", nameof(page));
            if (size < 1) throw new ArgumentException("Size must be greater than 0", nameof(size));

            try
            {
                var query = _context.Users
                    .AsNoTracking()
                    .AsQueryable();

                if (ShouldIncludeCustomer(orderBy))
                {
                    query = query.Include(u => u.Customer);
                }
                if (ShouldIncludeName(orderBy))
                {
                    query = query.Include(u => u.Name);
                }
                if (ShouldIncludeAddress(orderBy))
                {
                    query = query.Include(u => u.Address);
                }

                query = ApplyOrder(query, orderBy);

                var totalCount = await query.CountAsync();

                var items = await query
                    .Skip((page - 1) * size)
                    .Take(size)
                    .ToListAsync();

                return new PaginatedList<User>(items, totalCount, page, size);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private bool ShouldIncludeCustomer(string orderBy)
        {
            return !string.IsNullOrEmpty(orderBy) &&
                   orderBy.ToLower().Contains("customer");
        }

        private bool ShouldIncludeName(string orderBy)
        {
            return !string.IsNullOrEmpty(orderBy) &&
                   orderBy.ToLower().Contains("name");
        }

        private bool ShouldIncludeAddress(string orderBy)
        {
            return !string.IsNullOrEmpty(orderBy) &&
                   orderBy.ToLower().Contains("address");
        }

        private IQueryable<User> ApplyOrder(IQueryable<User> query, string orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return query.OrderBy(u => u.Username)
                           .ThenByDescending(u => u.Email);

            var orderParams = orderBy.ToLower().Split(',');
            var orderedQuery = query;
            bool firstOrder = true;

            foreach (var param in orderParams)
            {
                var trimmedParam = param.Trim();
                var parts = trimmedParam.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var field = parts[0];
                var direction = parts.Length > 1 ? parts[1] : "asc";

                orderedQuery = ApplyOrderToField(orderedQuery, field, direction, ref firstOrder);
            }

            return orderedQuery;
        }

        private IQueryable<User> ApplyOrderToField(IQueryable<User> query, string field, string direction, ref bool firstOrder)
        {
            switch (field.ToLower())
            {
                case "username":
                    return ApplyDirection(query, u => u.Username, direction, ref firstOrder);
                case "email":
                    return ApplyDirection(query, u => u.Email, direction, ref firstOrder);
                case "role":
                    return ApplyDirection(query, u => u.Role, direction, ref firstOrder);
                case "status":
                    return ApplyDirection(query, u => u.Status, direction, ref firstOrder);
                case "createdat":
                    return ApplyDirection(query, u => u.CreatedAt, direction, ref firstOrder);
                case "updatedat":
                    return ApplyDirection(query, u => u.UpdatedAt, direction, ref firstOrder);
                case "name":
                    return ApplyDirection(query,
                        u => u.Name != null ? u.Name.FirstName : string.Empty,
                        direction, ref firstOrder);
                case "address.city":
                    return ApplyDirection(query,
                        u => u.Address != null ? u.Address.City : string.Empty,
                        direction, ref firstOrder);

                case "address.street":
                    return ApplyDirection(query,
                        u => u.Address != null ? u.Address.Street : string.Empty,
                        direction, ref firstOrder);

                case "address.number":
                    return ApplyDirection(query,
                        u => u.Address != null ? u.Address.Number : 0,
                        direction, ref firstOrder);

                case "address.zipcode":
                    return ApplyDirection(query,
                        u => u.Address != null ? u.Address.ZipCode : string.Empty,
                        direction, ref firstOrder);

                case "address.geolocation":
                    return ApplyDirection(query,
                        u => u.Address != null && u.Address.Geolocation != null ? u.Address.Geolocation.ToString() : string.Empty,
                        direction, ref firstOrder);
                case "customer":
                    return ApplyDirection(query,
                        u => u.Customer != null ? u.Customer.ToString() : string.Empty,
                        direction, ref firstOrder);
                default:
                    return firstOrder ?
                        query.OrderBy(u => u.Username).ThenByDescending(u => u.Email) :
                        ((IOrderedQueryable<User>)query).ThenBy(u => u.Username).ThenByDescending(u => u.Email);
            }
        }

        private IQueryable<User> ApplyDirection<TKey>(
            IQueryable<User> query,
            Expression<Func<User, TKey>> keySelector,
            string direction,
            ref bool firstOrder)
        {
            if (firstOrder)
            {
                firstOrder = false;
                return direction == "desc" ?
                    query.OrderByDescending(keySelector) :
                    query.OrderBy(keySelector);
            }
            else
            {
                return direction == "desc" ?
                    ((IOrderedQueryable<User>)query).ThenByDescending(keySelector) :
                    ((IOrderedQueryable<User>)query).ThenBy(keySelector);
            }
        }

        public IQueryable<User> Query()
        {
            return _context.Users.AsQueryable();
        }
    }
}
