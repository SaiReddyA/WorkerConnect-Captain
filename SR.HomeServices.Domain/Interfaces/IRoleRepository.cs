using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SR.HomeServices.Domain.Interfaces
{
    /// <summary>
    /// Defines the contract for role-related data access operations.
    /// </summary>
    /// <remarks>
    /// Implementations of this interface are responsible for retrieving
    /// role information assigned to users from a data store. This abstraction
    /// allows the domain and application layers to remain decoupled from
    /// specific database technologies.
    /// </remarks>
    public interface IRoleRepository
    {
        /// <summary>
        /// Retrieves the roles associated with a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>
        /// A list of role names assigned to the user. If the user has no roles,
        /// the returned list will be empty.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="userId"/> is less than or equal to zero.
        /// </exception>
        Task<List<string>> GetRolesByUserIdAsync(int userId);
    }
}
