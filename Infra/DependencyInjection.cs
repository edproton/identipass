using Application.Repositories.Common;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using ILogger = Application.Services.ILogger;

namespace Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfra(
        this IServiceCollection services,
        EnvironmentKind environment,
        IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        services.AddSingleton<ILogger, Logger>();
        services.AddSingleton<ITokenService, TokenService>();
        services.AddSingleton<IPasswordService, PasswordService>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddDatabase(configuration);

        if (environment.IsDevelopment())
        {
            using var context = services.BuildServiceProvider().GetService<AppDbContext>()!;

            context.Database.Migrate();

            if (!context.Set<User>().Any() && !context.Set<Role>().Any())
            {
                var role = new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "System Admin",
                };

                context.Set<Role>().Add(role);
                context.Set<User>().Add(new User
                {
                    Email = "root@root.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("root"),
                    Roles = [role]
                });

                context.SaveChanges();
            }
        }

        return services;
    }

    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseConfiguration = configuration
            .GetRequiredSection("Database")
            .Get<DatabaseConfiguration>()
            ?.Validate();

        services.AddDbContext<AppDbContext>(op => op.UseNpgsql(databaseConfiguration!.ConnectionString));

        var repositoriesInterfaces = typeof(IGenericRepository<>).Assembly.GetTypes()
            .Where(t => t.IsInterface && t.Name.EndsWith("Repository"))
            .ToList();

        var repositoriesImplementations = typeof(GenericRepository<>).Assembly.GetTypes()
            .Where(t => t.IsClass && t.Name.EndsWith("Repository"))
            .ToList();

        foreach (var repositoryInterface in repositoriesInterfaces)
        {
            var repositoryImplementation = repositoriesImplementations
                .FirstOrDefault(t => t.Name == repositoryInterface.Name[1..]);

            if (repositoryImplementation is null)
            {
                throw new InvalidOperationException(
                    $"Repository implementation for {repositoryInterface.Name} not found");
            }

            services.AddScoped(repositoryInterface, repositoryImplementation);
        }
    }
}