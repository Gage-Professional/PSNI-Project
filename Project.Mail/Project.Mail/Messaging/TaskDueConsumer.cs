using MassTransit;
using Project.Messaging.Contracts;

namespace Project.Mail.Messaging;

public class TaskDueConsumer(IMailSender mailSender) : IConsumer<TaskDueMessage>
{
    public async Task Consume(ConsumeContext<TaskDueMessage> context)
    {
        bool isOverdue = DateTime.Now > context.Message.DueDate;

        string subject = $"Task {(isOverdue ? "Overdue" : "Due")}: {context.Message.Task}";
        string body = $"""{context.Message.AssignedToName ?? "User"}, task "{context.Message.Task}" is {(isOverdue ? "past due" : "due as soon as possible")}. Please take appropriate action.""";

        await mailSender.SendMailAsync(context.Message.AssignedToEmail, subject, body);
    }
}