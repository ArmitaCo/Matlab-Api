using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Matlab.Logger
{
    public static class SlackLogger
    {
        private static SlackClient _client;
        private static String _appName="Matlab";
        static SlackLogger()
        {
            _client = new SlackClient("https://hooks.slack.com/services/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");
            _client.PostMessage("Logger is start logging for "+_appName);
        }

        public static void PostMessage(LogLevel logLevel, Dictionary<LogProperties, object> dataDictionary)
        {
            Payload payload =new Payload()
            {
                Text = _appName,
                Attachments = new List<Attachment>(){new Attachment()
                {
                    Color = logLevel.GetColor(),
                    Fields = dataDictionary.OrderBy(x=>x.Key).Select(x=>new Field()
                    {
                        Short = true,
                        Title = x.Key.ToString(),
                        Value = x.Value.ToString()
                    }).ToList()
                }}
            };
            _client.PostMessage(payload);
        }

        public static String GetColor(this LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    return "#c7c5c4";
                case LogLevel.Info:
                    return "#94f2e5";
                case LogLevel.Warn:
                    return "#f5c36d";
                case LogLevel.Error:
                    return "#f76a68";
                case LogLevel.Fatal:
                    return "#fa0d00";
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
            }
        }
    }


    class SlackClient
    {
        private readonly Uri _uri;
        private readonly Encoding _encoding = new UTF8Encoding();

        public SlackClient(string urlWithAccessToken)
        {
            _uri = new Uri(urlWithAccessToken);
        }

//        //Post a message using simple strings
        public void PostMessage(string text, string username = null, string channel = null)
        {
            Payload payload = new Payload()
            {
                Channel = channel,
                Username = username,
                Text = text
            };

            PostMessage(payload);
        }

        //Post a message using a Payload object
        public void PostMessage(Payload payload)
        {
            string payloadJson = JsonConvert.SerializeObject(payload);

            using (WebClient client = new WebClient())
            {
                NameValueCollection data = new NameValueCollection();
                data["payload"] = payloadJson;

                var response = client.UploadValues(_uri, "POST", data);

                //The response text is usually "ok"
                string responseText = _encoding.GetString(response);
            }
        }
    }

    //This class serializes into the Json payload required by Slack Incoming WebHooks
    class Payload
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("attachments")]
        public List<Attachment> Attachments { get; set; }

    }

    class Attachment
    {
        [JsonProperty("fields")]
        public List<Field> Fields { get; set; }
        [JsonProperty("color")]
        public string Color { get; set; }
    }

    class Field
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("short")]
        public bool Short { get; set; }
    }
}
