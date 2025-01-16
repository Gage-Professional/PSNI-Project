using MassTransit;
using Project.Data.Models;
using Project.Messaging.Contracts;

namespace Project.Data;

public class TaskDueSweeper(IDataRepository<TaskEntity> repo, IPublishEndpoint endpoint) : BackgroundService
{
    private readonly TimeSpan _interval = TimeSpan.FromDays(1);

    public bool IsEnabled { get; set; } = true;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new(_interval);

        while (!stoppingToken.IsCancellationRequested &&
               IsEnabled &&
               await timer.WaitForNextTickAsync(stoppingToken))
        {
            IEnumerable<TaskEntity> tasksDue = await repo.ReadAllAsync(task =>
                task.CompletedOn == null &&
                task.DueDate != null &&
                DateTime.Now.Date >= task.DueDate.Value.Date);

            foreach (TaskEntity task in tasksDue)
            {
                TaskDueMessage message = new()
                {
                    Id = task.Id,
                    Task = task.Task,
                    Details = task.Details,
                    DueDate = task.DueDate.Value,
                    AssignedToName = task.AssignedToName,
                    AssignedToEmail = task.AssignedToEmail
                };

                await endpoint.Publish(message, stoppingToken);
            }
        }
    }
}