using System;
using NServiceBus;
using SimpleMatrix.InternalMessages.AccountManagement;


namespace SimpleMatrix.AccountManagement
{
    public partial class CreateAccountProcessor
    {
		
        partial void HandleImplementation(CreateAccount message)
        {
            // Implement your handler logic here.
            Console.WriteLine("AccountManagement received " + message.GetType().Name);
        }

    }
}
