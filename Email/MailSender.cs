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
                string ToAddress = "temitoyosi@gmail.com";
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
            //body += "     (message from: " + fromName + ",   email: " + senderEmail+")";
            //fromName = "michealogundero.com - " + fromName;
            //bool readWebConfig = false;
            //string emailStatus = string.Empty;
            //string from = "ogunderoayodeji@gmail.com";
            //string to = "ogunderoayodeji@gmail.com";

            //string bcc = "";
            //string userName = "devs.cervitech@gmail.com";
            //string password = "anelpo14";
            //Attachment attachment = null;

            //SmtpClient client = new SmtpClient();
            //client.EnableSsl = true; //Gmail works on Server Secured Layer
            //if (!readWebConfig)
            //{
            //    client.Credentials = new System.Net.NetworkCredential(userName, password);
            //    client.Port = 587; // Gmail works on this port
            //    client.Host = "smtp.gmail.com"; //hotmail: smtp.live.com
            //}
            //try
            //{
            //    using (MailMessage mail = new MailMessage())
            //    {
            //        mail.From = new MailAddress(from, fromName);
            //        mail.ReplyTo = new MailAddress(from, fromName);
            //        mail.Sender = mail.ReplyTo;
            //        if (to.Contains(";"))
            //        {
            //            string[] _EmailsTO = to.Split(";".ToCharArray());
            //            for (int i = 0; i < _EmailsTO.Length; i++)
            //            {
            //                mail.To.Add(new MailAddress(_EmailsTO[i]));
            //            }
            //        }
            //        else
            //        {
            //            if (!to.Equals(string.Empty))
            //            {
            //                mail.To.Add(new MailAddress(to));
            //            }
            //        }
            //        //CC

            //        //BCC
            //        if (bcc.Contains(";"))
            //        {
            //            string[] _EmailsBCC = bcc.Split(";".ToCharArray());
            //            for (int i = 0; i < _EmailsBCC.Length; i++)
            //            {
            //                mail.Bcc.Add(new MailAddress(_EmailsBCC[i]));
            //            }
            //        }
            //        else
            //        {
            //            if (!bcc.Equals(string.Empty))
            //            {
            //                mail.Bcc.Add(new MailAddress(bcc));
            //            }
            //        }

            //        mail.Subject = subject;
            //        mail.Body = body;
            //        mail.IsBodyHtml = true;
            //        //attachment
            //        if (attachment != null)
            //        {
            //            mail.Attachments.Add(attachment);
            //        }

            //        await client.SendMailAsync(mail);
            //        emailStatus = "success";
            //        return true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    emailStatus = ex.Message;
            //    return false;
            //} // end try 

        }
    }
}
