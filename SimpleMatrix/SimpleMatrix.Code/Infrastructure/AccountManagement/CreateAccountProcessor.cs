using System;
using NServiceBus;
using SimpleMatrix.InternalMessages.AccountManagement;


namespace SimpleMatrix.AccountManagement
{
    public partial class CreateAccountProcessor : IHandleMessages<CreateAccount>
    {
		
		public void Handle(CreateAccount message)
		{
			this.HandleImplementation(message);
		}

		partial void HandleImplementation(CreateAccount message);

		public IBus Bus { get; set; }

    }
}
