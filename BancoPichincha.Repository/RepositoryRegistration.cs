using BancoPichincha.Core.Interfaces.Repository;
using BancoPichincha.Core.Models.Entities;
using BancoPichincha.Repository.Data;
using BancoPichincha.Repository.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BancoPichincha.Repository
{
    public static class RepositoryRegistration
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ICuentaRepository, CuentaRepository>();
            services.AddScoped<IMovimientoRepository, MovimientoRepository>();

            services.AddDbContext<TestBancoPichinchaContext>(options => options.UseSqlServer(configuration.GetConnectionString("testBancoPichincha")));

            return services;
        }
    }
}
