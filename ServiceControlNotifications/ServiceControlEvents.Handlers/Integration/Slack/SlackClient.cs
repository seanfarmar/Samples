namespace ServiceControlEvents.Handlers.Integration.Slack
{
    using System;
    using System.Collections.Specialized;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class SlackClient
    {
        private readonly Encoding _encoding = new UTF8Encoding();
        private readonly Uri _uri;

        public SlackClient(string urlWithAccessToken)
        {
            _uri = new Uri(urlWithAccessToken);
        }

        //Post a message using simple strings
        public async Task<string> PostMessage(string text, string username = null, string channel = null)
        {
            var payload = new Payload
            {
                Channel = channel,
                Username = username,
                Text = text
            };

            return await PostMessage(payload)
                .ConfigureAwait(false);
        }

        //Post a message using a Payload object
        public async Task<string> PostMessage(Payload payload)
        {
            var payloadJson = JsonConvert.SerializeObject(payload);

            using (var client = new WebClient())
            {
                var data = new NameValueCollection();
                data["payload"] = payloadJson;

                var response =
                    await client.UploadValuesTaskAsync(_uri, "POST", data)
                        .ConfigureAwait(false);

                //The response text is usually "ok"
                return _encoding.GetString(response);
            }
        }
    }
}