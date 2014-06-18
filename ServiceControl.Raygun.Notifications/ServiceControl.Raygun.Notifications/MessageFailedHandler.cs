namespace NServiceBus.Operations.Notifications
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using Logging;
    using Mindscape.Raygun4Net;
    using ServiceControl.Contracts.MessageFailures;

    public class MessageFailedHandler : IHandleMessages<MessageFailed>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (MessageFailedHandler));

        private readonly RaygunClient _client = new RaygunClient("W6dvMkzLUhbNluhNVnCxaw==");

        readonly string _servicePulsBaseUrl = ConfigurationManager.AppSettings["ServicePulseUrl"];
        public IBus Bus { get; set; }

        public void Handle(MessageFailed message)
        {
            //for more info about the exception make calls to ServiceControl's http api: /api/errors/{message.message.FailedMessageId}   #returns full metadata for message


            var exceptionMessage = string.Format("{0} at endpoint {1} message id: {2}", message.FailureDetails.Exception.Message, message.EndpointId, message.FailedMessageId);

            Logger.Info(exceptionMessage);

            var customData = GetCustomData(message);

            _client.Send(new Exception(exceptionMessage), null,customData);            
        }

        private Dictionary<string, string> GetCustomData(MessageFailed message)
        {
            var serivicePuleUrlLink = string.Format(@"<a href=""{0}"">{0}</a>", _servicePulsBaseUrl);

            return new Dictionary<string, string>
            {
                {"ExceptionType", message.FailureDetails.Exception.ExceptionType},
                {"Message", message.FailureDetails.Exception.Message},
                {"Source", message.FailureDetails.Exception.Source},
                {"StackTrace", message.FailureDetails.Exception.StackTrace},
                {"TimeOfFailure", message.FailureDetails.TimeOfFailure.ToString(CultureInfo.InvariantCulture)},
                {"Endpoint", message.FailureDetails.AddressOfFailingEndpoint},
                {"EndpointId", message.EndpointId},
                {"FailedMessageId", message.FailedMessageId},
                {"ServicePulsBaseUrl", serivicePuleUrlLink}
            };
        }
    }
}