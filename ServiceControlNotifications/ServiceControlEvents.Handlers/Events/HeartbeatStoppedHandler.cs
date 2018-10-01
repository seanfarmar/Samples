namespace ServiceControlEvents.Handlers.Events
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Messages.Commands;
    using Microsoft.Azure;
    using NServiceBus;
    using ServiceControl.Contracts;

    public class HeartbeatStoppedHandler : IHandleMessages<HeartbeatStopped>
    {
        public async Task Handle(HeartbeatStopped message, IMessageHandlerContext context)
        {
            var endpointId = message.HostId + "-" + message.EndpointName;
            var pushNotificationToSlack = BuildSlackNotification(message);

            await context.SendLocal(pushNotificationToSlack)
                .ConfigureAwait(false);
        }

        public BuildSlackNotification BuildSlackNotification(HeartbeatStopped message)
        {
// Send notification to slack
            var attachments = new List<SlackAttachent>();
            var fields = new List<SlackAtachmentField>();

            var firstAttachment = new SlackAttachent
            {
                Color = "warning",
                Title = "Environment: " + CloudConfigurationManager.GetSetting("Environment"),
                Text = "",
                Footer = "======================================"
            };

            var field = new SlackAtachmentField
            {
                Title = "Host: ",
                Value = message.Host,
                Short = true
            };

            fields.Add(field);

            field = new SlackAtachmentField
            {
                Title = "Endpoint: ",
                Value = message.EndpointName,
                Short = true
            };

            fields.Add(field);

            field = new SlackAtachmentField
            {
                Title = "Last Received At: ",
                Value = message.LastReceivedAt.ToString("g"),
                Short = true
            };

            fields.Add(field);

            firstAttachment.AttachmentFields = fields;

            attachments.Add(firstAttachment);

            BuildSlackNotification pushNotificationToSlack = new BuildSlackNotification
            {
                Channel = "#tech",
                MarkDown = true,
                Username = "ServiceControl Events Monitor",
                Text = "ServiceControl :: Heartbeat Stopped Event"
            };

            pushNotificationToSlack.SlackAttachents = attachments;

            return pushNotificationToSlack;
        }
    }
}