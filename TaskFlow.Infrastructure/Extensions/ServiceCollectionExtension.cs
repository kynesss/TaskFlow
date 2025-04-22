using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskFlow.Domain.Interfaces;
using TaskFlow.Infrastructure.Mappings;
using TaskFlow.Infrastructure.Persistence;
using TaskFlow.Infrastructure.Repositories;

namespace TaskFlow.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddDefaultUI();

            services.AddScoped<IdentitySeeder>();
            services.AddScoped<ITaskItemRepository, TaskItemRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddAutoMapper(typeof(IdentityMappingProfile));
        }
    }
}