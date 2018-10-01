namespace ServiceControlEvents.Handlers.Integration.Slack
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    //A simple C# class to post messages to a Slack channel
    //Note: This class uses the Newtonsoft Json.NET serializer available via NuGet

    //This class serializes into the Json payload required by Slack Incoming WebHooks
    public class Payload
    {
        [JsonProperty("channel")] public string Channel { get; set; }

        [JsonProperty("username")] public string Username { get; set; }

        [JsonProperty("text")] public string Text { get; set; }

        [JsonProperty("color")] public string Color { get; set; }

        [JsonProperty("mrkdwn")] public bool MarkDown { get; set; } // "mrkdwn": true

        [JsonProperty("attachments")] public List<SlackAttachent> SlackAttachents { get; set; }
    }

    public class SlackAttachent
    {
        [JsonProperty("color")] public string Color { get; set; }

        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("text")] public string Text { get; set; }

        [JsonProperty("mrkdwn_in")] public string MarkdownIn { get; set; }

        [JsonProperty("fields")] public List<SlackAtachmentField> AttachmentFields { get; set; }
    }

    public class SlackAtachmentField
    {
        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("value")] public string Value { get; set; }

        [JsonProperty("short")] public bool Short { get; set; }
    }
}