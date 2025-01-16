using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Project.Messaging;

public static class MessagingConfiguration
{
    public static void AddMessaging(
        this IServiceCollection services,
        Action<IBusRegistrationConfigurator>? configureConsumers = null)
    {
        services.AddMassTransit(mt =>
        {
            mt.UsingRabbitMq((context, cfg) =>
            {
                configureConsumers?.Invoke(mt);
                cfg.ConfigureEndpoints(context);
                cfg.Host("rabbitmq://localhost", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.UseMessageRetry(cfgRetry =>
                {
                    cfgRetry.Interval(3, TimeSpan.FromSeconds(5));
                });
            });
        });
    }
}