using System;
using HalloWorld.Commands;
using NServiceBus;

namespace HalloWorld.MyEndpoint
{
    class SaySomthingHandler : IHandleMessages<SaySomething>
    {
        public void Handle(SaySomething message)
        {
            Console.WriteLine(message.What);
        }
    }
}
