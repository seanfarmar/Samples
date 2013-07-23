using System;
using NServiceBus;
 
namespace Amazon.Billing
{
	public class TransportConfig : INeedInitialization
	{
		public void Init()
		{
			// Tranport: Msmq (Default) - No configuration needed
		}
	}
}
