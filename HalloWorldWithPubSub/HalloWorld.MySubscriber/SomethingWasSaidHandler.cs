namespace HalloWorld.MySubscriber
{
    using System;
    using Events;
    using NServiceBus;

    public class SomethingWasSaidHandler : IHandleMessages<SomethingWasSaid>
    {
        public void Handle(SomethingWasSaid message)
        {
            Console.WriteLine("Handeled SomethingWasSaid message with : {0}", message.What);
        }
    }
}