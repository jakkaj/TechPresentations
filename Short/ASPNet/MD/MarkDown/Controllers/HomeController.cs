using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //https://raw.githubusercontent.com/jakkaj/xIOT/master/README.md
            return View();
        }


        public async Task<IActionResult> GetMarkdown(UrlModel model)
        {
            var hc = new HttpClient();

            var result = await hc.GetStringAsync(new Uri(model.Url));

            var markDownGetterModel = new MarkDownGetterModel
            {
                text = result
            };

            var serialised = JsonConvert.SerializeObject(markDownGetterModel);

           
            
            using (var message = new HttpRequestMessage(HttpMethod.Post, "https://api.github.com/markdown"))
            {
                message.Headers.Add("User-Agent",
                    "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.112 Safari/537.36");

                var content = new StringContent(serialised, Encoding.UTF8,"application/json");
                message.Content = content;

                using (var resultPost = await hc.SendAsync(message))
                {
                    if (resultPost.IsSuccessStatusCode)
                    {
                        if (resultPost.Content != null)
                        {
                            var resultText = await resultPost.Content.ReadAsStringAsync();
                            model.Result = resultText;
                            
                        }
                       
                    }
                    
                }
            }


            return View(model);


        }

    }
}
