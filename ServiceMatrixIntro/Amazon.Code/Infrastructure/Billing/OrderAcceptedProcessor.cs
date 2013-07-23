using System;
using NServiceBus;
using Amazon.Contracts.Sales;


namespace Amazon.Billing
{
    public partial class OrderAcceptedProcessor : IHandleMessages<OrderAccepted>
    {
		
		public void Handle(OrderAccepted message)
		{
			this.HandleImplementation(message);
		}

		partial void HandleImplementation(OrderAccepted message);

		public IBus Bus { get; set; }

    }
}
