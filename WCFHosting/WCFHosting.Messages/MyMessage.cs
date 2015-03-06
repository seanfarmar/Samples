namespace Messages
{
    using System;
    using NServiceBus;

    public class MyMessage : IMessage
    {
        public Guid IdGuid { get; set; }
        public string Name { get; set; }
    }
}