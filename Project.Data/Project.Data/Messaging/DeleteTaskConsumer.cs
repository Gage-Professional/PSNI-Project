using MassTransit;
using Project.Data.Models;
using Project.Messaging.Contracts;

namespace Project.Data.Messaging;

public class DeleteTaskConsumer(IDataRepository<TaskEntity> repo, IPublishEndpoint endpoint) : IConsumer<DeleteTaskMessage>
{
    public async Task Consume(ConsumeContext<DeleteTaskMessage> context)
    {
        TaskEntity? task = await repo.ReadAsync(context.Message.Id);
        if (task is null)
            return;

        bool wasDeleted = await repo.DeleteAsync(context.Message.Id);
        if (!wasDeleted)
            return;

        TaskDeletedMessage message = new()
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