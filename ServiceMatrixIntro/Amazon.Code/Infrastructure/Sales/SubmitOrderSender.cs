using System;
using NServiceBus;
using Amazon.InternalMessages.Sales;

namespace Amazon.Sales
{
    public partial class SubmitOrderSender: ISubmitOrderSender, NServiceBus.INServiceBusComponent
    {
        public void Send(SubmitOrder message)
		{
			Bus.Send(message);	
		}

        public IBus Bus { get; set; }
    }

    public interface ISubmitOrderSender
    {
        void Send(SubmitOrder message);
    }
}
