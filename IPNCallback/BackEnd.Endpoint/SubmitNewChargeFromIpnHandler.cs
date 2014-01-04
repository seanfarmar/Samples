namespace BackEnd.Endpoint
{
	using System;
	using Messages;
	using NServiceBus;

	public class SubmitNewChargeFromIpnHandler : IHandleMessages<SubmitNewChargeFromIpn>
	{
		public void Handle(SubmitNewChargeFromIpn message)
		{
			Console.WriteLine("Handeling SubmitNewChargeFromIpn Message with product name: {0} wit PostId: {1}", message.ProductName, message.PostId);
		}
	}
}
