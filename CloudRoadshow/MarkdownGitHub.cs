using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace RoadService.Demo
{
    public class MarkdownStuff
    {
        public static async Task<string> GetMarkdown(string url)
        {
            var hc = new HttpClient();

            var result = await hc.GetStringAsync(new Uri(url));

            var markDownGetterModel = new MarkDownGetterModel
            {
                text = result
            };

            var serialised = JsonConvert.SerializeObject(markDownGetterModel);

            var stringResult = "";

            using (var message = new HttpRequestMessage(HttpMethod.Post, "https://api.github.com/markdown"))
            {
                message.Headers.Add("User-Agent",
                    "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.112 Safari/537.36");
                message.Headers.Add("Accept",
                    "application/json");

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

        public class MarkDownGetterModel
        {
            public string text { get; set; }
            public string mode { get; set; } = "markdown";
        }

        public class UrlModel
        {
            public string Url { get; set; }
            public string Result { get; set; }
        }
    }

	public class MarkDownException : Exception
    {
        public MarkDownException()
        {

        }
    }
}