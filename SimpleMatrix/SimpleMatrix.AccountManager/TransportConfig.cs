using System;
using NServiceBus;
 
namespace SimpleMatrix.AccountManager
{
	public class TransportConfig : INeedInitialization
	{
		public void Init()
		{
			// Tranport: Msmq (Default) - No configuration needed
		}
	}
}
