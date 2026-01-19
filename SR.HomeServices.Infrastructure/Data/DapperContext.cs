namespace SR.HomeServices.Infrastructure.Data
{
    /// <summary>
    /// Provides database connection management for Dapper-based data access.
    /// </summary>
    /// <remarks>
    /// This class acts as a lightweight database context responsible for
    /// creating and configuring SQL connections using connection strings
    /// defined in the application configuration.
    /// </remarks>
    public sealed class DapperContext
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="DapperContext"/> class.
        /// </summary>
        /// <param name="configuration">
        /// The application configuration used to retrieve database connection strings.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="configuration"/> is null.
        /// </exception>
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration
                ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Creates and returns a new database connection instance.
        /// </summary>
        /// <returns>
        /// An <see cref="IDbConnection"/> configured with the default connection string.
        /// </returns>
        /// <remarks>
        /// The caller is responsible for opening and disposing the returned connection.
        /// </remarks>
        public IDbConnection CreateConnection()
        {
            var connectionString =
                _configuration.GetConnectionString("DefaultConnection");

            return new SqlConnection(connectionString);
        }
    }
}
