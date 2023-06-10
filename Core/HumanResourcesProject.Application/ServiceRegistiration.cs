using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HumanResourcesProject.Application
{
	public static class ServiceRegistiration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
