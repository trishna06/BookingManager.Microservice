using System;
using BookingManager.Infrastructure.Repositories;
using Microservice.Utility.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingManager.Infrastructure
{
    public class BookingManagerContextFactory : IDesignTimeDbContextFactory<BookingManagerContext>
    {
        public BookingManagerContext CreateDbContext(string[] args)
        {
            string connectionString = $"Server=localhost;Database=UEIOS_V4_DEV;User Id=arcstone;Password=Juniormints123;";

            DbContextOptionsBuilder<BookingManagerContext> optionsBuilder = new DbContextOptionsBuilder<BookingManagerContext>();
            optionsBuilder.UseSqlServer(connectionString, providerOptions =>
            {
                providerOptions.CommandTimeout(30);
                providerOptions.EnableRetryOnFailure(
                maxRetryCount: 3,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
                providerOptions.MigrationsHistoryTable($"{BookingManagerContext.TablePrefix}_EF_MIGRATIONS_HISTORY".ApplyConvention());
            }).ReplaceService<IHistoryRepository, CustomHistoryRepository>();

            return new BookingManagerContext(optionsBuilder.Options, null, null, null);
        }
    }
}
