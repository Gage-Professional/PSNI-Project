using MassTransit;
using Project.Messaging.Contracts;

namespace Project.Mail.Messaging;

public class TaskDeletedConsumer(IMailSender mailSender) : IConsumer<TaskDeletedMessage>
{
    public async Task Consume(ConsumeContext<TaskDeletedMessage> context)
    {
        string subject = $"Task Removed: {context.Message.Task}";
        string body = $"""{context.Message.AssignedToName ?? "User"}, task "{context.Message.Task}" is no longer assigned to you.""";

        await mailSender.SendMailAsync(context.Message.AssignedToEmail, subject, body);
    }
}