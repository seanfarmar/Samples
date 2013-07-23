using System;
using NServiceBus;
 
namespace Amazon.ECommerce
{
	public class TransportConfig : INeedInitialization
	{
		public void Init()
		{
			// Tranport: Msmq (Default) - No configuration needed
  		}
	}
}
