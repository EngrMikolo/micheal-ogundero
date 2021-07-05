using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using System.Configuration;

namespace MichealOgundero
{
    public class MailSender
    {
        public static async Task<string> SendEmail(string name, string message, string subject, string email)
        {
            try
            {
                string FromAddress = "contactmichealogundero@gmail.com";
                string password = ConfigurationManager.AppSettings["contactpassword"];
                if (password ==  null)
                {
                    password = "contactmichealogundero2021";
                }
                string FromAdressTitle = "Portfolio Contact Form";
                string ToAddress = "ogunderoayodeji@gmail.com";
                string Subject = "Portfolio Contact Form";
                string BodyContent = message;

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(FromAdressTitle, FromAddress));
                mimeMessage.To.Add(new MailboxAddress(ToAddress));
                mimeMessage.Subject = Subject;
                if (subject is null)
                {
                    subject = "No Subject";
                }
                mimeMessage.Body = new TextPart("html")
                {
                    Text = "<h2>"+subject +"</h2><br/> <h3>"+ BodyContent + "</h3><br/> <h4>This mail was sent from " + email + "(" + name + ")</h4>"
                };

                using (var emailClient = new SmtpClient())
                {

                    try
                    {
                        emailClient.SslProtocols |= SslProtocols.Tls;
                        emailClient.CheckCertificateRevocation = false;
                        await emailClient.ConnectAsync("smtp.gmail.com", 465, true);
                        await emailClient.AuthenticateAsync(FromAddress, password);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return "Gmail server failed to authenticate or connect.Sorry! cannot send mail";
                    }


                    await emailClient.SendAsync(mimeMessage);

                    await emailClient.DisconnectAsync(true);

                    //_logger.LogError("Email Sent successfully");
                }

                return "The mail has been sent successfully !!";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "sorry cannot send mail";
            }
         
        }
    }
}
