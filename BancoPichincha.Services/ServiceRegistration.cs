using BancoPichincha.Core.Interfaces.Service;
using BancoPichincha.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BancoPichincha.Services
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServicesProjectServices(this IServiceCollection services)
        {
            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<ICuentaService, CuentaService>();
            services.AddTransient<IMovimientoService, MovimientoService>();

            return services;
        }
    }
}
