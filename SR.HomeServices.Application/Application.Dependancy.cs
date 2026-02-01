global using Microsoft.Extensions.DependencyInjection;
global using SR.HomeServices.Application.Interfaces;
global using SR.HomeServices.Domain.Entities;
global using SR.HomeServices.Domain.Interfaces;
global using Microsoft.IdentityModel.Tokens;
global using System.Collections.Generic;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using SR.HomeServices.Application.DTOs;
global using System.ComponentModel.DataAnnotations;
global using System.Text;
global using Microsoft.Extensions.Configuration;
global using SR.HomeServices.Application.Security;
global using SR.HomeServices.Application.Services;
global using SR.HomeServices.Infrastructure;
global using SR.HomeServices.Infrastructure.Data;


namespace SR.HomeServices.Application
{
    /// <summary>
    /// Provides extension methods for registering application-layer
    /// services with the dependency injection container.
    /// </summary>
    /// <remarks>
    /// The application layer contains business logic and use cases.
    /// This class centralizes service registrations to keep the
    /// application startup configuration clean and maintainable.
    /// </remarks>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers application services and use-case implementations.
        /// </summary>
        /// <param name="services">
        /// The dependency injection service collection.
        /// </param>
        /// <returns>
        /// The same <see cref="IServiceCollection"/> instance to allow
        /// fluent chaining of service registrations.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="services"/> is null.
        /// </exception>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IBookingService, BookingService>();

            return services;
        }
    }
}
