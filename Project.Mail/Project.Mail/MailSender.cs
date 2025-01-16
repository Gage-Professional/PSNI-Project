using MailKit.Net.Smtp;
using MimeKit;

namespace Project.Mail;

public class MailSender : IMailSender
{
    public async Task SendMailAsync(string sendTo, string subject, string body)
    {
        // These are dummy values

        MimeMessage mail = new();
        mail.From.Add(new MailboxAddress("sender_name", "sender_address"));
        mail.To.Add(new MailboxAddress("recipient_name", sendTo));
        mail.Subject = subject;
        mail.Body = new TextPart("plain") { Text = body };

        using SmtpClient client = new();
        await client.ConnectAsync("host");
        await client.AuthenticateAsync("address", "password");
        await client.SendAsync(mail);
        await client.DisconnectAsync(true);
    }
}