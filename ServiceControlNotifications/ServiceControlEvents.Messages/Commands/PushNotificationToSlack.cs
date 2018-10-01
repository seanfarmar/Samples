namespace ServiceControlEvents.Messages.Commands
{
    using System.Collections.Generic;

    public class BuildSlackNotification
    {
        public string Channel { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public bool MarkDown { get; set; } // "mrkdwn": true
        public List<SlackAttachent> SlackAttachents { get; set; }
    }

    public class SlackAttachent
    {
        public string Color { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string MarkdownIn { get; set; }
        public List<SlackAtachmentField> AttachmentFields { get; set; }
        public string Footer { get; set; }
    }

    public class SlackAtachmentField
    {
        public string Title { get; set; }
        public string Value { get; set; }
        public bool Short { get; set; }
    }
}