using Project.Data;
using Project.Data.Messaging;
using Project.Data.Models;
using Project.Messaging;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<TaskDbContext>();
        services.AddScoped<IDataRepository<TaskEntity>, TaskRepository>();
        services.AddHostedService<TaskDueSweeper>();
        services.AddMessaging(cfg =>
        {
            cfg.AddConsumer<CompleteTaskConsumer>();
            cfg.AddConsumer<CreateTaskConsumer>();
            cfg.AddConsumer<DeleteTaskConsumer>();
            cfg.AddConsumer<UpdateTaskConsumer>();
        });
    })
    .Build();

await host.RunAsync();