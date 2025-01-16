using MassTransit;
using Project.Messaging.Contracts;

namespace Project.Mail.Messaging;

public class TaskCreatedConsumer(IMailSender mailSender) : IConsumer<TaskCreatedMessage>
{
    public async Task Consume(ConsumeContext<TaskCreatedMessage> context)
    {
        string subject = $"New Task: {context.Message.Task}";
        string body = $"""{context.Message.AssignedToName ?? "User"}, task "{context.Message.Task}" has been assigned to you. Please complete at your earliest convenience.""";

        await mailSender.SendMailAsync(context.Message.AssignedToEmail, subject, body);
    }
}