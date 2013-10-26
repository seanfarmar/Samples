namespace Server
{
    using System;
    using Messages;
    using NServiceBus;
    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        public void Handle(PlaceOrder message)
        {
            Console.WriteLine(@"Order for Product:{0} placed", message.Product);

            // throw new Exception("Uh oh - something went wrong....");
        }
    }
}