namespace Messages.Commands
{
    using System.Collections.Generic;
    using System.Collections.Specialized;

    public class SendSmtpEmail
    {
        public string To { get; set; }
        public ListDictionary Replacements { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string ReplyTo { get; set; }
        public IDictionary<string, string> Attachments { get; set; }
        public string UserProfileRequestId { get; set; }
        public string RequesterUserBusinessName { get; set; }
        public string RequesterUserSenderFullName { get; set; }
        public string RequesterUserSenderEmail { get; set; }
        public string RequestedUserRecipientFullName { get; set; }
        public string RequestedUserRecipientEmail { get; set; }
    }
}