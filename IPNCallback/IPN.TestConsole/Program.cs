namespace IPN.TestConsole
{
	using System;
	using System.Net.Http;

	class Program
	{
		private const string PostTestString = @"?ProductId=821594&ProductName=Pageonce+Gold";
			/*&contractId=3047232&contractName=Full+Version
&contractPrice=4.99&referenceNumber=65503442&transactionDate=03%2F13%2F2012+04%3A00+PM&
untilDate=04%2F13%2F2012+04%3A00+PM&transactionType=RECURRING&quantity=1&currency=USD&addCD=N
&referrer=&promoteContractsNum=0&originalReferenceNumber=64749580&invoiceAmount=4.99&paymentMethod=CC&paymentType=CC
&creditCardType=VISA&shippingMethod=&remoteAddress=
&invoiceInfoURL=https%3A%2F%2Fshoppers.BlueSnap.com%2Fjsp%2Forder_locator_info.jsp%3FrefId%3D862B2132737A285E%26acd%3D38811CABB1E6BDA1&testMode=N
&contractOwner=992556&invoiceAmountUSD=4.99&invoiceChargeCurrency=USD&invoiceChargeAmount=4.99&subscriptionId=21658700
&creditCardLastFourDigits=9986&creditCardExpDate=11%2F2015&plimusNode=1&invoiceTitle=&invoiceFirstName=Andres
&invoiceLastName=Gomez&invoiceCompany=&invoiceAddress1=&invoiceAddress2=&invoiceCity=&invoiceState=VA&invoiceCountry=US
&invoiceZipCode=22312&invoiceEmail=dre2640%40gmail.com&invoiceWorkPhone=&invoiceExtension=&invoiceFaxNumber=
&invoiceMobilePhone=&accountId=42450866&title=&firstName=Andres&lastName=Gomez&username=1326484883659&company=
&address1=&address2=&city=&state=VA&country=US&zipCode=22312&email=dre2640%40gmail.com&workPhone=&extension=&faxNumber=
&mobilePhone=&homePhone=&shippingFirstName=Andres&shippingLastName=Gomez&shippingAddress1=&shippingAddress2=&shippingCity=
&shippingState=VA&shippingCountry=US&shippingZipCode=22312&authKey=&custom1=3685319992595876003&custom2=9101370417534661015
&custom3=5000&couponCode=&licenseKey=&targetBalance=Plimus_ACCOUNT";*/

		static void Main(string[] args)
		{
			Console.WriteLine("Press 'Enter' to post a test.To exit, Ctrl + C");

			while (Console.ReadLine() != null)
			{
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri("http://localhost:8853");

					var postId = Guid.NewGuid();

					var result = client.PostAsJsonAsync("/api/charge" + PostTestString + "&PostId=" + postId,"We are nor really using this :)").Result;

					var statusCode = result.IsSuccessStatusCode ? "Complete Successfully" : "Failed to Complete Successfully";
					
					Console.WriteLine("Posting a sample IPN with id {0} {1}", postId, statusCode);
				}

				Console.WriteLine("==========================================================================");
				Console.WriteLine();
				Console.WriteLine("Press 'Enter' to post a test.To exit, Ctrl + C");
			}
		}
	}
}
