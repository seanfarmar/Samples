namespace WebServiceIntegration.DHL
{
    using System;
    using System.Net;
    using Nancy;
    using Nancy.ModelBinding;
    using HttpStatusCode = Nancy.HttpStatusCode;

    public class MainNancyModule : NancyModule
    {
        public MainNancyModule()
        {
            Get["/"] = x => "Hello World";

            Post["/api/DispatchOrder/"] = parameters =>
            {
                var item = this.Bind<DispachRequest>();

                var response = new Response {StatusCode = HttpStatusCode.OK};

                if (item.ThrowException) throw new WebException("oooops , we have a problem");

                if (item.Fail)
                    response.StatusCode = HttpStatusCode.BadRequest;

                return response;
            };
        }
    }

    public class DispachRequest
    {
        public bool Fail
        {
            get { return new Random().Next(2) == 0; }
        }

        public Guid OrderId { get; set; }
        public Guid DispatchId { get; set; }
        public string CountryCode { get; set; }
        public Guid DhlCustomerNumber { get; set; }
        public bool ThrowException { get; set; }
    }
}