using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sellify.Application.Models.Token;
using Sellify.Application.Persistence;
using Sellify.Infrastructure.Persistence.Repositories;



namespace Sellify.Infrastructure.Persistence;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices
    (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        return services;
    }
}