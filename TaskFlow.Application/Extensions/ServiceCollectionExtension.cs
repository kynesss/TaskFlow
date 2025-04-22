using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TaskFlow.Application.ApplicationUser;
using FluentValidation;
using FluentValidation.AspNetCore;
using TaskFlow.Application.TaskItem.Commands.CreateTaskItem;

namespace TaskFlow.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            services.AddAutoMapper(assemblies);
            services.AddMediatR(assemblies);

            services.AddScoped<IUserContext, UserContext>();

            services.AddValidatorsFromAssemblyContaining<CreateTaskItemCommand>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        }
    }
}