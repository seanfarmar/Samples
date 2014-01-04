namespace BackEnd.Messages
{
	using System;
	using NServiceBus;

	public class SubmitNewChargeFromIpn : ICommand
    {
	    public string ProductId { get; set; }

		public string ProductName { get; set; }
		
		public Guid PostId { get; set; }

		//....

    }
}
