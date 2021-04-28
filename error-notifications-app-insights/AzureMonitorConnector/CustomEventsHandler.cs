using System.Collections.Generic;
using System.Threading.Tasks;
using Messages.Commands;
using Microsoft.ApplicationInsights;
using NServiceBus;
using NServiceBus.Logging;

#region AzureMonitorConnectorEventsHandler

public class SendMessgeFalidToMonitorHandler :
    IHandleMessages<SendMessgeFalidToMonitor>
{
    readonly TelemetryClient telemetryClient;
    static ILog log = LogManager.GetLogger<SendMessgeFalidToMonitorHandler>();

    public SendMessgeFalidToMonitorHandler(TelemetryClient telemetryClient)
    {
        this.telemetryClient = telemetryClient;
    }

    public Task Handle(SendMessgeFalidToMonitor message, IMessageHandlerContext context)
    {
        telemetryClient.TrackEvent("Message Failed", new Dictionary<string, string>
        {
            {"MessageId", message.FailedMessageId},
        });

        log.Error($"Received NotificationsEndpoint 'MessageFailed' for a {message.GetType()} with ID {message.FailedMessageId}.");

        return Task.CompletedTask;
    }
} 
#endregion