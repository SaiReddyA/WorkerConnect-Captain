global using SR.HomeServices.Domain.Interfaces;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Data.SqlClient;
global using Dapper;
global using SR.HomeServices.Infrastructure.Data;
global using SR.HomeServices.Infrastructure.Repositories;
global using Microsoft.Extensions.DependencyInjection;
global using System;
global using System.Data;
global using System.Linq;
global using System.Threading.Tasks;
global using SR.HomeServices.Domain.Entities;


namespace SR.HomeServices.Infrastructure
{
    /// <summary>
    /// Provides extension methods for registering infrastructure-layer
    /// dependencies with the application's dependency injection container.
    /// </summary>
    /// <remarks>
    /// This class encapsulates all service registrations related to data access,
    /// repositories, and security implementations to keep startup configuration
    /// clean and maintainable.
    /// </remarks>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers infrastructure services, repositories, and security components.
        /// </summary>
        /// <param name="services">
        /// The dependency injection service collection.
        /// </param>
        /// <param name="configuration">
        /// The application configuration used by infrastructure services.
        /// </param>
        /// <returns>
        /// The same <see cref="IServiceCollection"/> instance for chaining.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="services"/> or <paramref name="configuration"/> is null.
        /// </exception>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));

            // Database context
            services.AddSingleton<DapperContext>();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();

            // Security
            return services;
        }
    }
}
