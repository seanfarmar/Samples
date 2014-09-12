namespace WebServiceIntegration.Shipping
{
    using System;
    using Messages.Commands;
    using Messages.Response;
    using NServiceBus;
    using System.Net;
    using System.Net.Http;
    
    class DispatchOrderToDhlHandler : IHandleMessages<DispatchOrderToDhl>
    {
        public IBus Bus { get; set; }
        private HttpClient HttpClient = new HttpClient();

        public void Handle(DispatchOrderToDhl message)
        {
            // call the web service
            // TODO: move to config
            const string uri = "http://localhost:12345/";
            const string url = "/api/DispatchOrder/";

            HttpClient.BaseAddress = new Uri(uri);
            
            var result = HttpClient.PostAsJsonAsync(url, message).Result;

            HttpStatusCode resultStatusCode = result.StatusCode;

            if (resultStatusCode == HttpStatusCode.OK)
            {
                // all good? do bus.reply with sucsess message
                Bus.Reply(new DispatchOrderToDhlSucsess { OrderId = message.OrderId, HttpStatusCode = resultStatusCode });
            }

            if (resultStatusCode != HttpStatusCode.OK)
            {
                // issues? do bus.reply with issue message
                Bus.Reply(new DispatchOrderToDhlFailure { OrderId = message.OrderId, HttpStatusCode = resultStatusCode });
            }
        }
    }
}
