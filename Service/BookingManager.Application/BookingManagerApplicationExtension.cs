using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BookingManager.Application.Behaviours;
using BookingManager.Application.Helpers;
using BookingManager.Application.Queries;
using EventBus.Utility.Events;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BookingManager.Application
{
    public static class BookingManagerApplicationExtension
    {
        public static IServiceCollection AddBookingManagerApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddBookingQueries();
            return services;
        }

        public static IServiceCollection AddBookingQueries(this IServiceCollection services)
        {
            services.AddTransient<KafkaProducerHelper>();
            services.AddTransient<IBookingQueries, BookingQueries>();
            return services;
        }
    }
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                   .AsImplementedInterfaces().InstancePerLifetimeScope();

            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(BookingManagerApplicationExtension).GetTypeInfo().Assembly)
                   .AsClosedTypesOf(typeof(IRequestHandler<>));

            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(BookingManagerApplicationExtension).GetTypeInfo().Assembly)
                   .AsClosedTypesOf(typeof(IRequestHandler<,>));

            // Register the DomainEventHandler classes (they implement INotificationHandler<>) in assembly holding the Domain Events
            builder.RegisterAssemblyTypes(typeof(BookingManagerApplicationExtension).GetTypeInfo().Assembly)
                   .AsClosedTypesOf(typeof(INotificationHandler<>));

            // Register the Command's Validators (Validators based on FluentValidation library)
            builder.RegisterAssemblyTypes(typeof(BookingManagerApplicationExtension).GetTypeInfo().Assembly)
                   .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                   .AsImplementedInterfaces();

            builder.Populate(new ServiceCollection());

            builder.RegisterGeneric(typeof(LoggingBehaviour<,>)).As(typeof(IPipelineBehavior<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(ValidatorBehaviour<,>)).As(typeof(IPipelineBehavior<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(TransactionBehaviour<,>)).As(typeof(IPipelineBehavior<,>)).InstancePerLifetimeScope();
        }
    }

    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(BookingManagerApplicationExtension).GetTypeInfo().Assembly)
                   .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
        }
    }
}
