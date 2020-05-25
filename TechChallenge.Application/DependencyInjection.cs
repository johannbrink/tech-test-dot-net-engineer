using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using TechChallenge.Application.Services;
using TechChallenge.Domain.Common;
using TechChallenge.Domain.Interfaces;

namespace TechChallenge.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<ILeadRepository, LeadRepository>();
            DomainEvents.Init();
            return services;
        }
    }
}
