namespace BackEnd.Messages
{
	using System;

	public class SampleIpn
	{
		public int ProductId { get; set; }

		public string ProductName { get; set; }

		public Guid PostId { get; set; }
	}
}