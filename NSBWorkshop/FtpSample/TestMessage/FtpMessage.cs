namespace TestMessage
{
    using System;
    using NServiceBus;

    [Serializable]
    public class FtpMessage : ICommand
    {
        public int Id { get; set;  }
        public String Name { get; set; }
    }
}
