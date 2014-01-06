namespace IPNCallback.Nancy
{
	using BackEnd.Messages;
	using global::Nancy;
	using global::Nancy.ModelBinding;
	using NServiceBus;

	public class HomeModule : NancyModule
	{
		public IBus Bus { get; set; }

		public HomeModule()
		{
			Get["/"] = p => View["index.html"];

			Post["/api"] = p =>
			{
				// if blueSnap post the data in the request body then we will be able to do simple model binding 

				// AFAIK Nancy doesn't support model binding from the query string		

				var chargeFromIpn = this.Bind<SubmitNewChargeFromIpn>();

				//var chargeFromIpn = new SubmitNewChargeFromIpn
				//{
				//	ProductId = this.Request.Query.ProductId,
				//	ProductName = this.Request.Query.ProductName,
				//	PostId = this.Request.Query.PostId
				//};

				SendIpnMessage(chargeFromIpn);

				return new Response()
				{
					StatusCode = HttpStatusCode.Accepted
				};
			};
		}

		private void SendIpnMessage(SubmitNewChargeFromIpn chargeFromIpn)
		{
			// bus.send with nancy self hosting in NServiceBus.hos process....

			//Bus.Send()...
		}
	}
}