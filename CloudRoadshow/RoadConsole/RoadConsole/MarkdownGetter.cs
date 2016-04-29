using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RoadConsole
{
    public class MarkdownGetter
    {
        public static async Task<string> GetMarkdown(string url)
        {
            var hc = new HttpClient();

            var urlSend = new UrlModel
            {
                Url = url
            };

            var serialised = JsonConvert.SerializeObject(urlSend);

            var stringResult = "";

            using (var message = new HttpRequestMessage(HttpMethod.Post, "http://jordosample.azurewebsites.net/api/markdown"))
            {
                message.Headers.Add("User-Agent",
                    "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.112 Safari/537.36");


                var content = new StringContent(serialised, Encoding.UTF8, "application/json");
                message.Content = content;

                using (var resultPost = await hc.SendAsync(message))
                {
                    if (resultPost.IsSuccessStatusCode)
                    {
                        if (resultPost.Content != null)
                        {
                            var resultText = await resultPost.Content.ReadAsStringAsync();
                            stringResult = resultText;

                        }

                    }

                }
            }

            return stringResult;
        }
    }

    public class UrlModel
    {
        public string Url { get; set; }
        public string Result { get; set; }
    }

}
