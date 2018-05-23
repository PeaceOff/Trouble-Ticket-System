using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace RestAPI.Services
{
    public class EmailSender
    {
        static string gmailUser = "TDINP2@gmail.com";
        static string gmailPassword = "TDINP2123";

        public void SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                // CLIENT
                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(EmailSender.gmailUser, EmailSender.gmailPassword);
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                // MESSAGE
                MailMessage mail = new MailMessage("tdinp2@noreply.com", toEmail);
                mail.Subject = subject;
                mail.Body = body;

                // SEND
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }
        }
    }
}