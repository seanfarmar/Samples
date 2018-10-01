namespace ServiceControlEvents.Handlers.Integration.Slack
{
    using System.Configuration;
    using System.Threading.Tasks;
    using AutoMapper;
    using Messages.Commands;
    using NServiceBus;

    internal class PushNotificationToSlackHandler : IHandleMessages<BuildSlackNotification>
    {
        public async Task Handle(BuildSlackNotification message, IMessageHandlerContext context)
        {
            var slackUrlWithAccessToken = ConfigurationManager.AppSettings["SlackUrlWithAccessToken"];

            var client = new SlackClient(slackUrlWithAccessToken);

            var payload = MapSlackMessagePayloadToPayload(message);

            await client.PostMessage(payload)
                .ConfigureAwait(false);
        }

        private Payload MapSlackMessagePayloadToPayload(Messages.Commands.BuildSlackNotification slackMessagePayload)
        {
            var config =
                new MapperConfiguration(cfg => cfg.CreateMap<Messages.Commands.BuildSlackNotification, Payload>());
            var mapper = config.CreateMapper();
            return mapper.Map<Payload>(slackMessagePayload);
        }
    }
}