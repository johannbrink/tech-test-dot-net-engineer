using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using TechChallenge.Domain.Common;

namespace TechChallenge.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            DomainEvents.Init();
        }
    }
}
