using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using Org.BouncyCastle.Crypto.Macs;


namespace EaglesTMS.Utility
{
    public class EmailSender : IEmailSender
    {
        //public string From { get; set; }
        //public string Smtp { get; set; }
        //public int Port { get; set; }
        //public string Password { get; set; }
        //public EmailSender(IConfiguration _config)
        //{
        //    From = _config.GetValue<string>("ForEmail:From");
        //    Smtp = _config.GetValue<string>("ForEmail:smtpClient");
        //    Port = _config.GetValue<int>("ForEmail:Port");
        //    Password = _config.GetValue<string>("ForEmail:EmailPass");
        //}
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailToSend = new MimeMessage();
            emailToSend.From.Add(MailboxAddress.Parse("info@egyeagles.com"));
            //emailToSend.From.Add(MailboxAddress.Parse("mohammed.khalph@gmail.com"));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };
            using (var emailClient = new SmtpClient())
            {
                //emailClient.Connect("mail.egyeagles.com", 465, MailKit.Security.SecureSocketOptions.StartTls);
                emailClient.Connect("mail.egyeagles.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                emailClient.Authenticate("info@egyeagles.com", "6PC@4LE1(hWQdPg");
                //emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                //emailClient.Authenticate("mohammed.khalph@gmail.com", "xjtddqpvgsikfbax");
                emailClient.Send(emailToSend);
                emailClient.Disconnect(true);
            }
            //MailMessage message = new MailMessage();
            //message.From = new MailAddress("mohammed.khalph@gmail.com");
            //message.To.Add(email);
            //message.Subject = subject;
            //message.Body = htmlMessage;

            //SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            //smtpClient.Credentials = new NetworkCredential("mohammed.khalph@gmail.com", "xjtddqpvgsikfbax");
            //smtpClient.EnableSsl = true;

            //smtpClient.Send(message);
            return Task.CompletedTask;
        }
    }
}
