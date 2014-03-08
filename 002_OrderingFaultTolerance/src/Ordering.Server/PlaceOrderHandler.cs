namespace Ordering.Server
{
    using System;
    using Messages;
    using NServiceBus;

    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        public IBus Bus { get; set; }

        public void Handle(PlaceOrder message)
        {
            Console.WriteLine(@"Order for Product:{0} placed with id: {1}", message.Product, message.Id);

            if (Bus.CurrentMessageContext.Headers.ContainsKey("Headers.Retries"))
            {
                var numberOfRetries = Bus.CurrentMessageContext.Headers[Headers.Retries];

                Console.WriteLine("SLR is kicked in, Bus.CurrentMessageContext.Headers[Headers.Retries]:{0} id: {1}", numberOfRetries,
                    message.Id);
            }

            // throw new Exception("Uh oh - something went wrong....");
        }
    }
}