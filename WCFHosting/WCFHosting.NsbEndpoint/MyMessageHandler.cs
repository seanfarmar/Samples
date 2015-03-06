namespace WCFHosting.NsbEndpoint
{
    using System;
    using Messages;
    using NServiceBus;

    internal class MyMessageHandler : IHandleMessages<MyMessage>
    {
        public void Handle(MyMessage message)
        {
            Console.WriteLine("Handeling message with Id: {0} and Name: {1}", message.IdGuid, message.Name);
        }
    }
}