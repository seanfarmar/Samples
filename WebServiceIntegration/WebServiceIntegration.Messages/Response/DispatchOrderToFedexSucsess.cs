namespace WebServiceIntegration.Messages.Response
{
    using System;
    using System.Net;

    public class DispatchOrderToFedexSucsess
    {
        public Guid OrderId { get; set; }
        public Guid DispatchId { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}