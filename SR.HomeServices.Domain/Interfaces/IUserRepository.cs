using SR.HomeServices.Domain.Entities;
using System.Threading.Tasks;

namespace SR.HomeServices.Domain.Interfaces
{
    /// <summary>
    /// Defines the contract for user-related data access operations.
    /// </summary>
    /// <remarks>
    /// Implementations of this interface are responsible for retrieving
    /// user information from a data store. This abstraction allows
    /// the domain and application layers to remain decoupled from
    /// specific database technologies.
    /// </remarks>
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user to retrieve.</param>
        /// <returns>
        /// A <see cref="User"/> object if a matching user is found; 
        /// otherwise, <c>null</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="email"/> is <c>null</c> or empty.
        /// </exception>
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task UpdatePasswordAsync(int userId, string passwordHash);
    }
}
