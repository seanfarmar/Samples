using System;
using NServiceBus;
 
namespace SimpleMatrix.WebSite
{
	public class TransportConfig : INeedInitialization
	{
		public void Init()
		{
			// Tranport: Msmq (Default) - No configuration needed
  		}
	}
}
