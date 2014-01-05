namespace IPNCallback.Controllers
{
	using System.Web.Http;
	using BackEnd.Messages;

	public class ChargeController : ApiController
	{
		// POST api/Charge
		public void Post([FromUri] SampleIpn ipn)
		{
			// parse the form and save the data
			var submitNewChargeFromIpnCommand = new SubmitNewChargeFromIpn { ProductId = ipn.ProductId, ProductName = ipn.ProductName, PostId = ipn.PostId };

			//post a command
			WebApiApplication.Bus.Send(submitNewChargeFromIpnCommand);
		}
	}
}