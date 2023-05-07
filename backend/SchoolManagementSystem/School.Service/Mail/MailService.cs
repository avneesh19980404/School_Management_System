using School.Common.Configurations;
using School.Model.Request;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace School.Service.Mail
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public bool sendEmail(MailRequest mailData)
        {

            try
            {
                // Command-line argument must be the SMTP host.
                SmtpClient client = new SmtpClient(_mailSettings.Host, _mailSettings.Port)
                {
                    Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password)
                };
                client.EnableSsl = true;
                // Specify the email sender.
                // Create a mailing address that includes a UTF8 character
                // in the display name.
                MailAddress from = new MailAddress(_mailSettings.Mail, _mailSettings.DisplayName, System.Text.Encoding.UTF8);
                // Set destinations for the email message.
                MailAddress to = new MailAddress(mailData.ToEmail);
                // Specify the message content.
                MailMessage message = new MailMessage(from, to);

                message.Body = mailData.Body;
                message.IsBodyHtml = true;
                // Include some non-ASCII characters in body and subject.
                string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
                message.Body += Environment.NewLine + someArrows;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Subject = mailData.Subject + someArrows;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                if (mailData.Attachments != null)
                {
                    byte[] attachment;
                    foreach (var file in mailData.Attachments)
                    {
                        if (file.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                file.CopyTo(ms);
                                attachment = ms.ToArray();

                            }
                            message.Attachments.Add(new Attachment(new MemoryStream(attachment), file.FileName, file.ContentType));
                        }
                    }
                }

                client.Send(message);
                message.Dispose();
                client.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
