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
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task Handle(DispatchOrderToDhl message, IMessageHandlerContext context)
        {
            HttpResponseMessage result = null;

            // call the web service
            Task.Run(() => { result = GetWebPageHtmlSizeAsync(message).Result; }).Wait();

            Console.Out.WriteLine(result);

            HttpStatusCode resultStatusCode = result.StatusCode;

            if (resultStatusCode == HttpStatusCode.OK)
            {
                // all good? do bus.reply with success message
                await context.Reply(new DispatchOrderToDhlSucsess
                {
                    OrderId = message.OrderId,
                    HttpStatusCode = resultStatusCode,
                    DispatchId = message.DispatchId
                }).ConfigureAwait(false);
            }

            if (resultStatusCode != HttpStatusCode.OK)
            {
                // issues? do bus.reply with issue message
                await context.Reply(new DispatchOrderToDhlFailure
                {
                    OrderId = message.OrderId,
                    HttpStatusCode = resultStatusCode,
                    DispatchId = message.DispatchId
                }).ConfigureAwait(false);
            }
        }

        private async Task<HttpResponseMessage> GetWebPageHtmlSizeAsync(DispatchOrderToDhl message)
        {
            const string uri = "http://localhost:12345/";
            const string url = "/api/DispatchOrder/";

            _httpClient.BaseAddress = new Uri(uri);

            return await _httpClient.PostAsJsonAsync(url, message)
                .ConfigureAwait(false);
        }
    }
}