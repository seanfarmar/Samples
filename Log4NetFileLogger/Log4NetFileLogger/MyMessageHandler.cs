using System;
using NServiceBus;
using log4net;

namespace Log4NetFileLogger
{
    public class MyMessageHandler : IHandleMessages<MyMessage>
    {
        private static readonly ILog Logger = LogManager.GetLogger("Name");

        public void Handle(MyMessage message)
        {
            var logMessage = string.Format("Handeled message with Id: {0}", message.Id);

            Console.WriteLine(logMessage);

            Logger.Info(logMessage);
        }
    }
}
