using System;
using NServiceBus;

namespace Log4NetFileLogger
{
    public class MyMessage : ICommand
    {
        public Guid Id { get; set; } 
    }
}
