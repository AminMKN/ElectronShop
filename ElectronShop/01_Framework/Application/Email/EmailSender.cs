using System.Net;
using System.Net.Mail;

namespace _01_Framework.Application.Email
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmail(string toEmail, string subject, string message, bool isBodyHtml)
        {
            using var client = new SmtpClient();
            var credentials = new NetworkCredential()
            {
                UserName = "aspemail007", // without @gmail.com
                Password = "AminAsp007"
            };

            client.Credentials = credentials;
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;

            using var emailMessage = new MailMessage()
            {
                To = { new MailAddress(toEmail) },
                From = new MailAddress("aspemail007@gmail.com"), // with @gmail.com
                Subject = subject,
                Body = message,
                IsBodyHtml = isBodyHtml
            };

            client.Send(emailMessage);
            return Task.CompletedTask;
        }
    }
}
