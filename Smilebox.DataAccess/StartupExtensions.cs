using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Smilebox.Data.Contracts.Abstractions;

namespace Smilebox.Data.EntityFramework
{
    public static class StartupExtensions
    {
        private static readonly string MigrationsAssembly =
            typeof(ApiDbContext).GetTypeInfo().Assembly.GetName().Name;

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("TestApiDb");

            return services.AddEntityFrameworkSqlServer().AddDbContextPool<ApiDbContext>(
                    options => options.UseSqlServer(connectionString, opt =>
                            opt.MigrationsAssembly(MigrationsAssembly))
                        .EnableSensitiveDataLogging(Debugger.IsAttached))
                .AddTransient<IUnitOfWork, UnitOfWork>(sp =>
                    new UnitOfWork(sp.GetRequiredService<ApiDbContext>()));
        }

        public static void ApplyDbMigrations(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApiDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}