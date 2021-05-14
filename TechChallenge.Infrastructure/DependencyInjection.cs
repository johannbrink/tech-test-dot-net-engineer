using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechChallenge.Application;
using TechChallenge.Infrastructure.Persistence;
using TechChallenge.Infrastructure.Repositories;

namespace TechChallenge.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseMySQL(configuration.GetConnectionString("DefaultConnection")));
                services.AddScoped(provider => provider.GetService<ApplicationDbContext>());
                services.AddTransient<ILeadRepository, LeadRepository>();
            return services;
        }
    }
}
