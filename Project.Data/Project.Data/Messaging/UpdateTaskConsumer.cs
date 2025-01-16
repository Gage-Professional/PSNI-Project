using MassTransit;
using Project.Data.Models;
using Project.Messaging.Contracts;

namespace Project.Data.Messaging;

public class UpdateTaskConsumer(IDataRepository<TaskEntity> repo, IPublishEndpoint endpoint) : IConsumer<UpdateTaskMessage>
{
    public async Task Consume(ConsumeContext<UpdateTaskMessage> context)
    {
        TaskEntity task = new()
        {
            Task = context.Message.Task,
            Details = context.Message.Details,
            DueDate = context.Message.DueDate,
            AssignedToName = context.Message.AssignedToName,
            AssignedToEmail = context.Message.AssignedToEmail
        };

        bool wasUpdated = await repo.UpdateAsync(task);
        if (!wasUpdated)
            return;

        TaskUpdatedMessage message = new()
        {
            Id = task.Id,
            Task = task.Task,
            Details = task.Details,
            DueDate = task.DueDate,
            CompletedOn = task.CompletedOn,
            AssignedToName = task.AssignedToName,
            AssignedToEmail = task.AssignedToEmail
        };

        await endpoint.Publish(message);
    }
}