namespace MonitoringNotifications.ServiceControl.Notifications
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using global::ServiceControl.Contracts.MessageFailures;
    using Messages.Commands;
    using NServiceBus;
    using NServiceBus.Logging;

    class MessageFailedHandler:IHandleMessages<MessageFailed>
    {
        static readonly ILog Logger = LogManager.GetLogger(typeof(MessageFailedHandler));
        public IBus Bus { get; set; }

        public void Handle(MessageFailed message)
        {
            Logger.InfoFormat("Message with id {0} failed with reason {1}", message.FailedMessageId, message.FailureDetails.Exception.Message);

            //for more info about the exception make calls to ServiceControl's http api: /api/errors/{message.message.FailedMessageId}   #returns full metadata for message
            // TODO: move to config
            const string serviceControlToken = "344b8642ce5e23aa8fef0ffd0cd126";

            var messageBody = string.Format("Message with id {0} failed with reason: '{1}' /n, go to http://particular.net:9090/#/failedMessages", message.FailedMessageId, message.FailureDetails.Exception.Message);
            
            // TODO: move to config
            const string url = "rooms/message";

            // TODO: move to config
            const string uri = "https://api.hipchat.com/v1/";
            // call hipchat
            // https://www.hipchat.com/docs/api/method/rooms/message
            // https://api.hipchat.com/v1/rooms/message

            var formParameters = new Dictionary<string, string>
                {
                    {"format", "json"},
                    {"auth_token", serviceControlToken},
                    {"message",messageBody},
                    {"room_id","398432"},
                    {"from","Notifier"},
                    {"message_format","text"},
                    {"notify", "1"},
                    {"color","purple"},
                };

            HttpStatusCode resultStatusCode;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);

                HttpContent content = new FormUrlEncodedContent(formParameters);

                var result = client.PostAsync(url, content).Result;

                resultStatusCode = result.StatusCode;
            }

            if (resultStatusCode != HttpStatusCode.OK)
            {
                Logger.InfoFormat("Failed to send the message to hipchat: Message with id {0} failed with reason {1}", message.FailedMessageId, message.FailureDetails.Exception.Message);
            }

            // send an email
            const string operationsEmail = "production@particular.net";

            var parameters = new Dictionary<string, string>
            {
                {"Context", messageBody},
            };

            Bus.Send(new SendNotificationEmail
            {
                To = operationsEmail,
                UserId = Guid.NewGuid(),
                EmailBodyTemplateId = "NotificationOfApplicationError",
                Parameters = parameters
            });

            // TODO: Add a call to pager duty
        }
    }
}