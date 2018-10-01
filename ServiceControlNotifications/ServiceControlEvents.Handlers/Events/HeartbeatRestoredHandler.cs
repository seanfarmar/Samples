namespace ServiceControlEvents.Handlers.Events
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Messages.Commands;
    using Microsoft.Azure;
    using NServiceBus;
    using ServiceControl.Contracts;

    public class HeartbeatRestoredHandler : IHandleMessages<HeartbeatRestored>
    {
        public async Task Handle(HeartbeatRestored message, IMessageHandlerContext context)
        {

            var endpointId = message.HostId + "-" + message.EndpointName;
            // Send notification to slack
            var pushNotificationToSlack = BuildSlackNotification(message);

            await context.SendLocal(pushNotificationToSlack)
                .ConfigureAwait(false);
        }

        public BuildSlackNotification BuildSlackNotification(HeartbeatRestored message)
        {
            var attachments = new List<SlackAttachent>();
            var fields = new List<SlackAtachmentField>();

            var firstAttachment = new SlackAttachent
            {
                Color = "#36a64f",
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
                Title = "Restored At: ",
                Value = message.RestoredAt.ToString("g"),
                Short = true
            };

            fields.Add(field);

            firstAttachment.AttachmentFields = fields;

            attachments.Add(firstAttachment);

            var pushNotificationToSlack = new BuildSlackNotification
            {
                Channel = "#tech",
                MarkDown = true,
                Username = "ServiceControl Events Monitor",
                Text = "ServiceControl :: Heartbeat Restored Event"
            };

            pushNotificationToSlack.SlackAttachents = attachments;

            return pushNotificationToSlack;
        }
    }
}