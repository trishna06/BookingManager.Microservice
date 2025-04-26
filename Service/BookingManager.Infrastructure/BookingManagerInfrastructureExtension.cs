using System;
using BookingManager.Infrastructure.Queries;
using BookingManager.Infrastructure.Repositories;
using Microservice.Utility.Domain.SeedWork;
using Microservice.Utility.Infrastructure.Extensions;
using Microservice.Utility.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace BookingManager.Infrastructure
{
    public static class BookingManagerInfrastructureExtension
    {
        public static IServiceCollection AddBookingManagerInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BookingManagerContext>(options =>
            {
                options.UseSqlServer(connectionString, providerOptions =>
                {
                    providerOptions.CommandTimeout(30);
                    providerOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                    providerOptions.MigrationsHistoryTable($"{BookingManagerContext.TablePrefix}_EF_MIGRATIONS_HISTORY".ApplyConvention());
                }).ReplaceService<IHistoryRepository, CustomHistoryRepository>();
            }, ServiceLifetime.Scoped);

            services.AddBookingManagerRepositories()
                    .AddCustomFieldServices();

            return services;
        }

        public static IServiceCollection AddBookingManagerRepositories(this IServiceCollection services)
        {
            services.AddScoped<IContextQuery, BookingManagerContextQuery>();
            return services;
        }

        public static IServiceCollection AddCustomFieldServices(this IServiceCollection services)
        {
            services.AddSingleton<ICustomFieldManager, CustomFieldManager>();

            return services;
        }
    }
}

