using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MichealOgundero
{
    public class MailSender
    {
        public static async Task<bool> SendEmail(string fromName, string body, string subject, string senderEmail)
        {
            body += "     (message from: " + fromName + ",   email: " + senderEmail+")";
            fromName = "michealogundero.com - " + fromName;
            bool readWebConfig = false;
            string emailStatus = string.Empty;
            string from = "ogunderoayodeji@gmail.com";
            string to = "ogunderoayodeji@gmail.com";

            string bcc = "";
            string userName = "devs.cervitech@gmail.com";
            string password = "anelpo14";
            Attachment attachment = null;

            SmtpClient client = new SmtpClient();
            client.EnableSsl = true; //Gmail works on Server Secured Layer
            if (!readWebConfig)
            {
                client.Credentials = new System.Net.NetworkCredential(userName, password);
                client.Port = 587; // Gmail works on this port
                client.Host = "smtp.gmail.com"; //hotmail: smtp.live.com
            }
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(from, fromName);
                    mail.ReplyTo = new MailAddress(from, fromName);
                    mail.Sender = mail.ReplyTo;
                    if (to.Contains(";"))
                    {
                        string[] _EmailsTO = to.Split(";".ToCharArray());
                        for (int i = 0; i < _EmailsTO.Length; i++)
                        {
                            mail.To.Add(new MailAddress(_EmailsTO[i]));
                        }
                    }
                    else
                    {
                        if (!to.Equals(string.Empty))
                        {
                            mail.To.Add(new MailAddress(to));
                        }
                    }
                    //CC

                    //BCC
                    if (bcc.Contains(";"))
                    {
                        string[] _EmailsBCC = bcc.Split(";".ToCharArray());
                        for (int i = 0; i < _EmailsBCC.Length; i++)
                        {
                            mail.Bcc.Add(new MailAddress(_EmailsBCC[i]));
                        }
                    }
                    else
                    {
                        if (!bcc.Equals(string.Empty))
                        {
                            mail.Bcc.Add(new MailAddress(bcc));
                        }
                    }

                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    //attachment
                    if (attachment != null)
                    {
                        mail.Attachments.Add(attachment);
                    }

                    await client.SendMailAsync(mail);
                    emailStatus = "success";
                    return true;
                }
            }
            catch (Exception ex)
            {
                emailStatus = ex.Message;
                return false;
            } // end try 

        }
    }
}
