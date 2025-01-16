using MassTransit;
using Project.Messaging.Contracts;

namespace Project.Mail.Messaging;

public class TaskUpdatedConsumer(IMailSender mailSender) : IConsumer<TaskUpdatedMessage>
{
    public async Task Consume(ConsumeContext<TaskUpdatedMessage> context)
    {
        string subject = $"Task Changed: {context.Message.Task}";
        string body = $"""{context.Message.AssignedToName ?? "User"}, task "{context.Message.Task}" has been changed. Please reference the new task and adjust accordingly.""";

        await mailSender.SendMailAsync(context.Message.AssignedToEmail, subject, body);
    }
}