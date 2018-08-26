using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OneRosterProviderDemo.Models;

namespace OneRosterProviderDemo
{
    public static class MigrationHelper
    {
        public static void EnsureDatabasesMigratedAndSeeded(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<ApiContext>();
                dbContext.Database.Migrate();
            }
        }
    }
}
