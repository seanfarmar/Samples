namespace WebServiceIntegration.DHL
{
    using System;
    using Nancy;

    public class MainNancyModule : NancyModule
    {
        public MainNancyModule()
        {
            Get["/"] = x => "Hello World";

            Post["/DispatchOrder/"] = parameters =>
            {
                var response = new Response {StatusCode = HttpStatusCode.OK};

                var item = new DispachRequest(Request.Form);
                
                if (item.Fail)
                    response.StatusCode = HttpStatusCode.BadRequest;

                return response;
            };
        }
    }

    public class DispachRequest
    {
        public bool Fail;

        public DispachRequest(dynamic form)
        {
            OrderId = form.OrderId;
            DispatchId = form.DispatchId;
            CountryCode = form.CountryCode;
            DhlCustomerNumber = form.DhlCustomerNumber;
            ThrowException = form.ThrowException;

            Fail = ThrowException;
        }

        public Guid OrderId { get; set; }
        public Guid DispatchId { get; set; }
        public string CountryCode { get; set; }
        public Guid DhlCustomerNumber { get; set; }
        public bool ThrowException { get; set; }
    }
}
