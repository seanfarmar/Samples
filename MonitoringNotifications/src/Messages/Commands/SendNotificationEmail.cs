namespace MonitoringNotifications.Messages.Commands
{
    using System;
    using System.Collections.Generic;
    using NServiceBus;

    public class SendNotificationEmail : ICommand
    {
        public Guid UserId { get; set; }
        public string To { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public string Title { get; set; }
        public string EmailBodyTemplateId { get; set; }
        public string From { get; set; }
        public string SmtpPassword { get; set; }
        public string SmtpUser { get; set; }
        public string CC { get; set; }
        public string ReplyTo { get; set; }
        public List<Attachment> Attachments;

        public class Attachment
        {
            public string FileName { get; set; }
            public string Content { get; set; }
        }
    }
}