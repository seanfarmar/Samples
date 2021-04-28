using System;
using System.Text;
using System.Threading.Tasks;
using Messages.Commands;
using NServiceBus;
using NServiceBus.Faults;
using NServiceBus.Logging;

#region subscriptions

public static class SubscribeToNotifications
{
    static ILog log = LogManager.GetLogger(typeof(SubscribeToNotifications));
    private static IEndpointInstance endpointInstance;

    public static async Task Subscribe(EndpointConfiguration endpointConfiguration)
    {
        #region telemetry send only endpoint
        
        EndpointConfiguration telemetryEndpointConfiguration = new EndpointConfiguration("telemetry");
        var transport = telemetryEndpointConfiguration.UseTransport<LearningTransport>();

        var conventions = telemetryEndpointConfiguration.Conventions();
        conventions.DefiningCommandsAs(type => type.Namespace != null && type.Namespace == "Messages.Commands");

        var routeting = transport.Routing();
        routeting.RouteToEndpoint(
            messageType: typeof(SendMessgeFalidToMonitor),
            destination: "AzureMonitorConnector");

        telemetryEndpointConfiguration.SendOnly();

        endpointInstance = await Endpoint.Start(telemetryEndpointConfiguration)
            .ConfigureAwait(false);
        
        #endregion

        var recoverability = endpointConfiguration.Recoverability();

        recoverability.Immediate(settings => settings.OnMessageBeingRetried(Log));
        recoverability.Delayed(settings => settings.OnMessageBeingRetried(Log));
        recoverability.Failed(settings => settings.OnMessageSentToErrorQueue(Log));
        recoverability.Failed(settings => settings.OnMessageSentToErrorQueue(SendTelemetry));
    }

    private async static Task SendTelemetry(FailedMessage failed)
    {
        log.Fatal($@"Message sent to error queue.
        Body: {GetMessageString(failed.Body)}");

        var message = new SendMessgeFalidToMonitor 
        {
            FailedMessageId = failed.MessageId,
            FaildMessageBody = GetMessageString(failed.Body),
            FaildMessageExceptionMessage = failed.Exception.Message,
            FaildMessageHeaders = failed.Headers,
        };

        await endpointInstance.Send(message)
            .ConfigureAwait(false);

    }

    static string GetMessageString(byte[] body)
    {
        return Encoding.UTF8.GetString(body);
    }

    static Task Log(FailedMessage failed)
    {
        log.Fatal($@"Message sent to error queue.
        Body:
        {GetMessageString(failed.Body)}");
        return Task.CompletedTask;
    }

    static Task Log(DelayedRetryMessage retry)
    {
        log.Fatal($@"Message sent to Delayed Retries.
        RetryAttempt:{retry.RetryAttempt}
        Body:
        {GetMessageString(retry.Body)}");
        return Task.CompletedTask;
    }

    static Task Log(ImmediateRetryMessage retry)
    {
        log.Fatal($@"Message sent to Immediate Retry.
        RetryAttempt:{retry.RetryAttempt}
        Body:
        {GetMessageString(retry.Body)}");
        return Task.CompletedTask;
    }
}

#endregion
