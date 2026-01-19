namespace SR.HomeServices.Infrastructure.Repositories
{
    /// <summary>
    /// Provides data access operations related to user role assignments.
    /// </summary>
    /// <remarks>
    /// This repository uses Dapper to retrieve role information
    /// associated with users from the database.
    /// </remarks>
    public sealed class RoleRepository : IRoleRepository
    {
        private readonly DapperContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The Dapper database context used to create database connections.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="context"/> is null.
        /// </exception>
        public RoleRepository(DapperContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Retrieves the roles assigned to a specific user.
        /// </summary>
        /// <param name="userId">
        /// The unique identifier of the user.
        /// </param>
        /// <returns>
        /// A list of role names assigned to the user.
        /// </returns>
        public async Task<List<string>> GetRolesByUserIdAsync(int userId)
        {
            const string sql = "SELECT r.RoleName FROM Roles r INNER JOIN UserRoles ur ON r.RoleId = ur.RoleId WHERE ur.UserId = @UserId";

            using var connection = _context.CreateConnection();

            var roles = await connection.QueryAsync<string>(sql, new { UserId = userId });

            return roles.ToList();
        }

    }
}
