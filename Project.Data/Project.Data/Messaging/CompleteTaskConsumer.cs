using MassTransit;
using Project.Data.Models;
using Project.Messaging.Contracts;

namespace Project.Data.Messaging;

public class CompleteTaskConsumer(IDataRepository<TaskEntity> repo, IPublishEndpoint endpoint) : IConsumer<CompleteTaskMessage>
{
    public async Task Consume(ConsumeContext<CompleteTaskMessage> context)
    {
        TaskEntity? task = await repo.ReadAsync(context.Message.Id);
        if (task is null)
            return;

        task.CompletedOn = DateTime.Now;

        bool isUpdated = await repo.UpdateAsync(task);
        if (!isUpdated)
            return;

        TaskCompletedMessage message = new()
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