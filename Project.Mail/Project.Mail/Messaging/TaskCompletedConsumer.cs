using MassTransit;
using Project.Messaging.Contracts;

namespace Project.Mail.Messaging;

public class TaskCompletedConsumer(IMailSender mailSender) : IConsumer<TaskCompletedMessage>
{
    public async Task Consume(ConsumeContext<TaskCompletedMessage> context)
    {
        string subject = $"Task Completed: {context.Message.Task}";
        string body = $"""{context.Message.AssignedToName ?? "User"}, thank you for your work on "{context.Message.Task}." The task has been marked as completed.""";

        await mailSender.SendMailAsync(context.Message.AssignedToEmail, subject, body);
    }
}