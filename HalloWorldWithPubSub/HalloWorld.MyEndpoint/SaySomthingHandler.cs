namespace HalloWorld.MyEndpoint
{
    using System;
    using Commands;
    using Events;
    using NServiceBus;

    class SaySomthingHandler : IHandleMessages<SaySomething>
    {
        public IBus Bus { get; set; }

        public void Handle(SaySomething message)
        {
            Console.WriteLine(message.What);

            Bus.Publish<SomethingWasSaid>(m => { m.What = "Hallo publisher"; });

            Console.WriteLine("Published SomethingWasSaid message.");
        }
    }
}
