using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechChallenge.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using TechChallenge.Application.Interfaces;

namespace TechChallenge.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseMySQL(configuration.GetConnectionString("DefaultConnection")));
                services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            return services;
        }
    }
}
