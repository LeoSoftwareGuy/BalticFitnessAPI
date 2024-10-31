using Application.Support;
using Application.Support.Behaviours;
using Application.Support.Interfaces;
using Application.Trainings.Commands.SaveMealCommand;
using Application.Trainings.Commands.SaveTrainingCommand;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddValidatorsFromAssemblyContaining<SaveMealCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<SaveTrainingCommandValidator>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
