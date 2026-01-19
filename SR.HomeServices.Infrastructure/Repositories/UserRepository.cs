
namespace SR.HomeServices.Infrastructure.Repositories
{
    /// <summary>
    /// Provides data access operations related to <see cref="User"/> entities.
    /// </summary>
    /// <remarks>
    /// This repository uses Dapper for lightweight and efficient
    /// database access and executes raw SQL queries.
    /// </remarks>
    public sealed class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The Dapper database context used to create connections.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="context"/> is null.
        /// </exception>
        public UserRepository(DapperContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Retrieves a user by email address.
        /// </summary>
        /// <param name="email">
        /// The email address associated with the user.
        /// </param>
        /// <returns>
        /// A <see cref="User"/> instance if found; otherwise, <c>null</c>.
        /// </returns>
        public async Task<User?> GetByEmailAsync(string email)
        {
            const string sql = """ SELECT *  FROM Users   WHERE Email = @Email  """;

            using var connection = _context.CreateConnection();

            return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Email = email });
        }

        public async Task AddAsync(User user)
        {
            const string sql = @"
            INSERT INTO Users (Email, Phone, PasswordHash, CreatedBy, UpdatedBy)
            VALUES (@Email, @Phone, @PasswordHash, @CreatedBy, @UpdatedBy);
            SELECT CAST(SCOPE_IDENTITY() as int);
        ";

            using var connection = _context.CreateConnection();
            user.UserId = await connection.QuerySingleAsync<int>(sql, new {Email =user.Email, PasswordHash = user.PasswordHash, Phone = user.Phone , CreatedBy = "Admin", UpdatedBy = "Admin"});

            const string getRoleSql = @"SELECT RoleId FROM Roles WHERE RoleName = @RoleName";

            var defaultRoleId = await connection.QuerySingleOrDefaultAsync<int>(getRoleSql, new { RoleName = "Captain" });

            if (defaultRoleId == 0)
                throw new Exception("Default role 'Captain' not found in Roles table.");
            const string roleSql = @" INSERT INTO UserRoles (UserId, RoleId, IsActive, CreatedBy, CreatedDate)
                VALUES (@UserId, @RoleId, @IsActive, @CreatedBy, GETDATE());";

            await connection.ExecuteAsync(roleSql, new
            {
                UserId = user.UserId,
                RoleId = defaultRoleId,
                IsActive = true,
                CreatedBy = "Admin"
            });
        }
        public async Task UpdatePasswordAsync(int userId, string passwordHash)
        {
            const string sql = @"UPDATE Users SET PasswordHash = @PasswordHash WHERE UserId = @UserId";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(sql, new { UserId = userId, PasswordHash = passwordHash });
        }
    }
}
