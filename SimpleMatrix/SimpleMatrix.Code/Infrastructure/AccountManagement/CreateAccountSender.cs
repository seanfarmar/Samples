using System;
using NServiceBus;
using SimpleMatrix.InternalMessages.AccountManagement;

namespace SimpleMatrix.AccountManagement
{
    public partial class CreateAccountSender: ICreateAccountSender, NServiceBus.INServiceBusComponent
    {
        public void Send(CreateAccount message)
		{
			Bus.Send(message);	
		}

        public IBus Bus { get; set; }
    }

    public interface ICreateAccountSender
    {
        void Send(CreateAccount message);
    }
}
