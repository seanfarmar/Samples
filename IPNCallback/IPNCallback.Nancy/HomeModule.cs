using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

using NServiceBus;

using Nancy.ModelBinding;

namespace IPNCallback.Nancy
{
	using System;
	using BackEnd.Messages;
	using global::Nancy;

	public class HomeModule : NancyModule
	{
		public HomeModule()
		{
            /*public IBus Bus{ get; set;
		    }*/

		    Get["/"] = p => View["index.html"];

			Post["/api"] = p =>
			{
				// if blueSnap post the data in the request body then we will be able to do simple model binding 

				// AFAIK Nancy doesn't support model binding from the query string
			    //var chargeFromIpns = this.Bind<SubmitNewChargeFromIpn>();

				/*var chargeFromIpn = new SubmitNewChargeFromIpn
				{
					ProductId = this.Request.Query.ProductId,
					ProductName = this.Request.Query.ProductName,
					PostId = this.Request.Query.PostId
				};*/
			    var chargeFromIpn = GetChargeFromRequest(Request.Query);

                ProcessIpnMessage(chargeFromIpn);

				return new Response()
				{
					StatusCode = HttpStatusCode.Accepted
				};
			};
		}

	    private SubmitNewChargeFromIpn GetChargeFromRequest(DynamicDictionary query)
	    {
	        var res = new SubmitNewChargeFromIpn();
	        var type = res.GetType();

	        foreach (var propertyName in query.Keys)
	        {
	            var pi = type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

	            if (pi == null)
	            {
	                continue;
	            }

	            switch (Type.GetTypeCode(pi.PropertyType))
	            {
	                case TypeCode.String:
	                    pi.SetValue(res, query[propertyName].Value);
	                    break;

	                case TypeCode.Boolean:
	                    pi.SetValue(res, query[propertyName].Value == "Y");
	                    break;

	                case TypeCode.DateTime:
	                    pi.SetValue(res, DateTime.ParseExact(query[propertyName].Value, "MM/dd/yyyy hh:mm tt", CultureInfo.InvariantCulture));
	                    break;

	                default:
	                {
	                    //much complex type than String    
	                    var val = Activator.CreateInstance(pi.PropertyType);
	                    MethodInfo mi = pi.PropertyType.GetMethod("Parse", new[] { typeof(string) });

	                    if (mi != null)
	                    {
	                        val = mi.Invoke(null, new object[] { query[propertyName].Value });
	                    }

	                    pi.SetValue(res, val);
	                }
	                    break;
	            }
	        }

	        return res;
	    }

	    private void ProcessIpnMessage(SubmitNewChargeFromIpn chargeFromIpn)
	    {
	        // bus.send with nancy self hosting in NServiceBus.hos process....
	        var order = GetIssueStandardLicense(chargeFromIpn);
            
            //Test validation. Will be used in test to check if all required fields are filled
            order.IsValid();

	        SendIssueStandardLicense(order);
	    }

	    private static IssueStandardLicense GetIssueStandardLicense(SubmitNewChargeFromIpn chargeFromIpn)
	    {
	        var order = new IssueStandardLicense
	                    {
	                        UniqueOrderId = chargeFromIpn.ReferenceNumber.ToString(),
	                        LicenseQuantity = chargeFromIpn.Quantity,
	                        OrderDate = chargeFromIpn.TransactionDate,
	                        UpgradeProtectionValidUntil = chargeFromIpn.UntilDate, //!?orderDate.AddYears(1).AddDays(-1),
	                        PlimusProduct = string.Format("{0} - {1}", chargeFromIpn.ProductId, chargeFromIpn.ProductName),
	                        LicenseType = chargeFromIpn.ProductName,
	                        //!?ParticularProducts = ParseParticularProduct(),
	                        PlimusContract = string.Format("{0} - {1}", chargeFromIpn.ContractId, chargeFromIpn.ContractName),
	                        Total = chargeFromIpn.InvoiceAmountUsd,
	                        Currency = "USD",
	                        //!?PlimusOrderNumber =originalReference,
	                        PlimusLinkForReceipt = chargeFromIpn.ShopperOrderUrl,
	                        //!?Properties = ParseParticularProperties(),
	                        Buyer = new Buyer
	                                {
	                                    CompanyName = chargeFromIpn.Company,
	                                    Address1 = chargeFromIpn.Address1,
	                                    Address2 = chargeFromIpn.Address2,
	                                    City = chargeFromIpn.City,
	                                    StateProvince = chargeFromIpn.State,
	                                    ZipPostalCode = chargeFromIpn.ZipCode,
	                                    Country = chargeFromIpn.Country,
	                                    ContactFirstName = chargeFromIpn.FirstName,
	                                    ContactLastName = chargeFromIpn.LastName,
	                                    ContactEmail = chargeFromIpn.Email,
	                                    ContactPhone = chargeFromIpn.MobilePhone, //!? there are a lots of phones
	                                    IpAddress = chargeFromIpn.RemoteAddress
	                                }
	                    };
	        return order;
	    }

	    private void SendIssueStandardLicense(IssueStandardLicense order)
	    {
	        
	    }
	}
}