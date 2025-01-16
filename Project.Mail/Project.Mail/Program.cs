using Project.Mail;
using Project.Mail.Messaging;
using Project.Messaging;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddScoped<IMailSender, MailSender>();
        services.AddMessaging(cfg =>
        {
            cfg.AddConsumer<TaskCompletedConsumer>();
            cfg.AddConsumer<TaskCreatedConsumer>();
            cfg.AddConsumer<TaskDeletedConsumer>();
            cfg.AddConsumer<TaskDueConsumer>();
            cfg.AddConsumer<TaskUpdatedConsumer>();
        });
    })
    .Build();

await host.RunAsync();