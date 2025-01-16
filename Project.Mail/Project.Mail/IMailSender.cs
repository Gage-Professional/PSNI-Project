namespace Project.Mail;

public interface IMailSender
{
    Task SendMailAsync(string sendTo, string subject, string body);
}