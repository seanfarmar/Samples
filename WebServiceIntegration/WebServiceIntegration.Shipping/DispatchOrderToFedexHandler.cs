namespace WebServiceIntegration.Shipping
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Messages.Commands;
    using Messages.Response;
    using NServiceBus;

    internal class DispatchOrderToFedexHandler : IHandleMessages<DispatchOrderToFedex>
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task Handle(DispatchOrderToFedex message, IMessageHandlerContext context)
        {
            HttpResponseMessage result = null;

            // call the web service
            Task.Run(() => { result = GetWebPageHtmlSizeAsync(message).Result; }).Wait();

            Console.Out.WriteLine(result);

            HttpStatusCode resultStatusCode = result.StatusCode;

            if (resultStatusCode == HttpStatusCode.OK)
            {
                // all good? do bus.reply with success message
                await context.Reply(new DispatchOrderToFedexSucsess
                {
                    OrderId = message.OrderId,
                    HttpStatusCode = resultStatusCode,
                    DispatchId = message.DispatchId
                }).ConfigureAwait(false);
            }

            if (resultStatusCode != HttpStatusCode.OK)
            {
                // issues? do bus.reply with issue message
                await context.Reply(new DispatchOrderToFedexFailure
                {
                    OrderId = message.OrderId,
                    HttpStatusCode = resultStatusCode,
                    DispatchId = message.DispatchId
                }).ConfigureAwait(false);
            }
        }

        private async Task<HttpResponseMessage> GetWebPageHtmlSizeAsync(DispatchOrderToFedex message)
        {
            const string uri = "http://localhost:12346/"; // fedex api
            const string url = "/api/DispatchOrder/";

            _httpClient.BaseAddress = new Uri(uri);

            return await _httpClient.PostAsJsonAsync(url, message)
                .ConfigureAwait(false);
        }
    }
}