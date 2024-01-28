using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sellify.Application.Contracts.Identity;
using Sellify.Application.Contracts.Infrasctructure;
using Sellify.Application.Models.Email;
using Sellify.Application.Models.ImageManagement;
using Sellify.Application.Models.Payment;
using Sellify.Application.Models.Token;
using Sellify.Application.Persistence;
using Sellify.Infrastructure.MessageImplementation;
using Sellify.Infrastructure.Persistence.Repositories;
using Sellify.Infrastructure.Services.Auth;



namespace Sellify.Infrastructure.Persistence;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
                                                                    IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IAuthService, AuthService>();

        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.Configure<StripeSettings>(configuration.GetSection("StripeSettings"));
        return services;
    }
}