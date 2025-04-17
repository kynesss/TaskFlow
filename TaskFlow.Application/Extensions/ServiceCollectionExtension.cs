using Microsoft.Extensions.DependencyInjection;
using TaskFlow.Application.ApplicationUser;

namespace TaskFlow.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            services.AddAutoMapper(assemblies);

            services.AddScoped<IUserContext, UserContext>();
        }
    }
}