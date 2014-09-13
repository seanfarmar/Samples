namespace WebServiceIntegration.Shipping
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Messages.Commands;
    using Messages.Response;
    using NServiceBus;

    internal class DispatchOrderToDhlHandler : IHandleMessages<DispatchOrderToDhl>
    {
        private readonly HttpClient HttpClient = new HttpClient();
        public IBus Bus { get; set; }

        public void Handle(DispatchOrderToDhl message)
        {
            HttpResponseMessage result = null;

            // call the web service
            Task.Run(() => { result = GetWebPageHtmlSizeAsync(message).Result; }).Wait();

            Console.Out.WriteLine(result);

            HttpStatusCode resultStatusCode = result.StatusCode;

            if (resultStatusCode == HttpStatusCode.OK)
            {
                // all good? do bus.reply with sucsess message
                Bus.Reply(new DispatchOrderToDhlSucsess
                {
                    OrderId = message.OrderId,
                    HttpStatusCode = resultStatusCode,
                    DispatchId = message.DispatchId
                });
            }

            if (resultStatusCode != HttpStatusCode.OK)
            {
                // issues? do bus.reply with issue message
                Bus.Reply(new DispatchOrderToDhlFailure
                {
                    OrderId = message.OrderId,
                    HttpStatusCode = resultStatusCode,
                    DispatchId = message.DispatchId
                });
            }
        }

        private async Task<HttpResponseMessage> GetWebPageHtmlSizeAsync(DispatchOrderToDhl message)
        {
            const string uri = "http://localhost:12345/";
            const string url = "/api/DispatchOrder/";

            HttpClient.BaseAddress = new Uri(uri);

            return await HttpClient.PostAsJsonAsync(url, message);
        }
    }
}