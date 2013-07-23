using System;
using NServiceBus;
using Amazon.InternalMessages.Sales;

namespace Amazon.Sales
{
    public partial class SubmitOrderProcessor : IHandleMessages<SubmitOrder>
    {
		public void Handle(SubmitOrder message)
		{
			this.HandleImplementation(message);

			this.Bus.Publish<Amazon.Contracts.Sales.OrderAccepted>(
                e => { /* set properties on e in here */ });
		}

		partial void HandleImplementation(SubmitOrder message);

		public IBus Bus { get; set; }
    }
}
