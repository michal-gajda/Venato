namespace Venato.Infrastructure;

using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Venato.Infrastructure.Rfid;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddOptions<RfidOptions>().BindConfiguration(RfidOptions.SectionName).ValidateDataAnnotations().ValidateOnStart();
        services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<RfidOptions>>().Value);
        services.AddHostedService<RfidWorker>();
        
        return services;
    }
}
