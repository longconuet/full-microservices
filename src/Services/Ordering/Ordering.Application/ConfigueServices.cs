using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Common.Behaviors;
using System.Reflection;

namespace Ordering.Application
{
    public static class ConfigueServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddMediatR(config =>
                {
                    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                    config.AddOpenBehavior(typeof(UnhandledExceptionBehavior<,>));
                    config.AddOpenBehavior(typeof(PerformanceBehavior<,>));
                    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                });

            return services;
        }
    }
}
