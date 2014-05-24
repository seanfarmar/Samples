namespace MonitoringNotifications.Operations.Notifications.Email
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Net.Mime;
    using System.Reflection;
    using System.Text;
    using Messages.Commands;
    using NServiceBus;
    using NServiceBus.Logging;
    using NServiceBus.ObjectBuilder;

    public class SendNotificationEmailHandler : IHandleMessages<SendNotificationEmail>
    {
        public IBuilder Builder { get; set; }
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SendNotificationEmail));

        public void Handle(SendNotificationEmail message)
        {
            var supperssExternal = (bool)new AppSettingsReader().GetValue("SupperssExternal", typeof(bool));      

            Logger.DebugFormat("Handeling SendNotificationEmail to: {0} and Context {1}", message.To, message.Parameters["Context"]);

            if (supperssExternal) return; 

            var templateParameters = message.Parameters;
    
            var body = ApplyTemplate(message.EmailBodyTemplateId, templateParameters);
            var title = message.Title;
            if (string.IsNullOrWhiteSpace(title))
                title = ExtractTitle(body);

            var email = message.To;

            if (string.IsNullOrEmpty(email))
            {
                if (templateParameters.Keys.Contains("Email"))
                    email = templateParameters["Email"];
                else
                {
                    Logger.Error("While handling SendEmail message, no TO field was provided ");
                    return;
                }
            }
            var smtpServer = ConfigurationManager.AppSettings["SmtpServer"];
            var smtpPassword = ConfigurationManager.AppSettings["SmtpPassword"];
            var smtpUser = ConfigurationManager.AppSettings["SmtpUser"];
            var port = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
            var useSsl = bool.Parse(ConfigurationManager.AppSettings["SmtpEnableSsl"]);
            using (var mail = new SmtpClient())
            {
                var mailMessage = new MailMessage();

                mail.Host = smtpServer;
                mail.EnableSsl = true;
                mail.Port = port;
                mail.EnableSsl = useSsl;

                if (!string.IsNullOrEmpty(smtpUser))
                {
                    mail.UseDefaultCredentials = false;
                    mail.Credentials = new NetworkCredential(smtpUser, smtpPassword);                    
                }
                else
                {
                    mail.UseDefaultCredentials = true;
                }

                
                if (message.SmtpPassword != null && message.SmtpUser != null)
                {
                    mail.Credentials = new NetworkCredential(message.SmtpUser, message.SmtpPassword);
                    mail.UseDefaultCredentials = false;
                }

                if (message.From != null)
                {
                    mailMessage.From = new MailAddress(message.From, ConfigurationManager.AppSettings["DefaultFromDisplayName"]);
                }
                else
                {
                    mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["DefaultFromEmail"], ConfigurationManager.AppSettings["DefaultFromDisplayName"]);
                }

                mailMessage.Subject = title;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = body;
                mailMessage.To.Add(email);

                if (message.ReplyTo != null)
                {
                    mailMessage.ReplyToList.Add(message.ReplyTo);
                }

                if (!string.IsNullOrWhiteSpace(message.CC))
                {
                    mailMessage.CC.Add(message.CC);
                }


                if ((message.Attachments != null) && (message.Attachments.Count > 0))
                {
                    foreach (var file in from attachment in message.Attachments
                                         where !string.IsNullOrWhiteSpace(attachment.FileName) && !string.IsNullOrWhiteSpace(attachment.Content)
                                         select new Attachment(CreateStream(attachment.Content), attachment.FileName, MediaTypeNames.Text.Xml))
                    {
                        mailMessage.Attachments.Add(file);
                    }
                }

                Logger.DebugFormat("Sending an email...");

                // mail.Send(mailMessage);
            }
        }

        private static string ExtractTitle(string body)
        {
            var first = body.IndexOf("<title>", StringComparison.Ordinal) + "<title>".Length;
            var last = body.LastIndexOf("</title>", StringComparison.Ordinal);
            var title = body.Substring(first, last - first);
            return string.IsNullOrWhiteSpace(title) ? "NServiceBus Customer Care" : title;
        }

        string ApplyTemplate(string templateId, Dictionary<string, string> parameters)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Format("MonitoringNotifications.Operations.Notifications.Email.Templates.{0}.html", templateId)))
            using (var reader = new StreamReader(stream))
            {
                var template = reader.ReadToEnd();

                return parameters.Aggregate(template, (current, parameter) => current.Replace("{" + parameter.Key.ToLower() + "}", parameter.Value));
            }
        }

        private static Stream CreateStream(string license)
        {
            var buffer = Encoding.UTF8.GetBytes(license);
            var ms = new MemoryStream();

            ms.Write(buffer, 0, buffer.Length);
            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);

            return ms;
        }
    }
}