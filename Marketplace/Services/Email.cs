using System.Net;
using System.Net.Mail;

namespace Forum_MVC.Helpers
{
    public static class Email
    {
        public static void Send(string email, string body, string subject)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("z.v.marketplace0@gmail.com", "Passw0rd%"),
                EnableSsl = true,
            };
            
            var mailMessage = new MailMessage
            {
                From = new MailAddress("z.v.marketplace0@gmail.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            smtpClient.Send(mailMessage);
        }
    }
}