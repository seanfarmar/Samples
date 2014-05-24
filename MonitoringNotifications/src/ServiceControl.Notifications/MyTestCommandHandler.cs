namespace MonitoringNotifications.ServiceControl.Notifications
{
    using Messages.Commands;
    using NServiceBus;
    using NServiceBus.Logging;

    class MyTestCommandHandler : IHandleMessages<MyTestCommand>
    {
        static readonly ILog Logger = LogManager.GetLogger(typeof(MyTestCommandHandler));
        public IBus Bus { get; set; }

        public void Handle(MyTestCommand message)
        {
            Logger.InfoFormat("Hndeling message with id: {0} with name: {1}", message.IdGuid, message.Name);         
        }
    }
}