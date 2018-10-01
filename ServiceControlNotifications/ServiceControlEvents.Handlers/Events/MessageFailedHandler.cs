namespace ServiceControlEvents.Handlers.Events
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Threading.Tasks;
    using Messages.Commands;
    using Microsoft.Azure;
    using NServiceBus;
    using ServiceControl.Contracts;

    public class MessageFailedHandler : IHandleMessages<MessageFailed>
    {
        public async Task Handle(MessageFailed message, IMessageHandlerContext context)
        {
            // builds Slack notification
            var slackNotification = PushNotificationToSlack(message);

            await context.SendLocal(slackNotification)
                .ConfigureAwait(false);
        }

        public BuildSlackNotification PushNotificationToSlack(MessageFailed message)
        {
            var pushNotificationToSlack = new BuildSlackNotification
            {
                Channel = "#tech",
                MarkDown = true,
                Username = "ServiceControl Events Monitor",
                Text = "ServiceControl :: Failed Message Event"
            };


            var attachments = new List<SlackAttachent>();

            var firstAttachment = new SlackAttachent
            {
                Color = "danger",
                Title = "Environment: " + CloudConfigurationManager.GetSetting("Environment"),
                Text = ""
            };

            var field = new SlackAtachmentField
            {
                Title = "Host",
                Value = message.ProcessingEndpoint.Host,
                Short = true
            };

            List<SlackAtachmentField> fields = new List<SlackAtachmentField> {field};

            field = new SlackAtachmentField
            {
                Title = "Endpoint",
                Value = message.ProcessingEndpoint.Name,
                Short = true
            };

            fields.Add(field);

            field = new SlackAtachmentField
            {
                Title = "Messgae Type",
                Value = message.MessageType,
                Short = true
            };

            fields.Add(field);

            field = new SlackAtachmentField
            {
                Title = "Time Of Failiur",
                Value = message.FailureDetails.TimeOfFailure.ToString("G"),
                Short = true
            };

            fields.Add(field);

            var deploymentEnvironment = ConfigurationManager.AppSettings["Environment"];

            field = new SlackAtachmentField
            {
                Title = "Link To Dashboard",
                Value = string.Format(
                    @"https://pv-{0}-monitor.westeurope.cloudapp.azure.com/#/failed-messages/message/{1}",
                    deploymentEnvironment, message.FailedMessageId),
                Short = false
            };

            fields.Add(field);

            field = new SlackAtachmentField
            {
                Title = "Exception Message",
                Value = message.FailureDetails.Exception.Message,
                Short = false
            };

            fields.Add(field);

            firstAttachment.AttachmentFields = fields;
            attachments.Add(firstAttachment);

            pushNotificationToSlack.SlackAttachents = attachments;
            return pushNotificationToSlack;
        }
    }
}