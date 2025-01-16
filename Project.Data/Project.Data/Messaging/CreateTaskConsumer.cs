using MassTransit;
using Project.Data.Models;
using Project.Messaging.Contracts;

namespace Project.Data.Messaging;

public class CreateTaskConsumer(IDataRepository<TaskEntity> repo, IPublishEndpoint endpoint) : IConsumer<CreateTaskMessage>
{
    public async Task Consume(ConsumeContext<CreateTaskMessage> context)
    {
        TaskEntity task = new()
        {
            Task = context.Message.Task,
            Details = context.Message.Details,
            DueDate = context.Message.DueDate,
            AssignedToName = context.Message.AssignedToName,
            AssignedToEmail = context.Message.AssignedToEmail
        };

        bool wasCreated = await repo.CreateAsync(task);
        if (!wasCreated)
            return;

        TaskCreatedMessage message = new()
        {
            Id = task.Id,
            Task = task.Task,
            Details = task.Details,
            DueDate = task.DueDate,
            AssignedToName = task.AssignedToName,
            AssignedToEmail = task.AssignedToEmail
        };

        await endpoint.Publish(message);
    }
}