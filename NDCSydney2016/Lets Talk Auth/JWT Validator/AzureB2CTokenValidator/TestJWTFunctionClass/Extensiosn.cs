using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestJWTFunctionClass
{
    public static class Extensiosn
    {
        public static async Task<TEntityType> GetAndParse<TEntityType>(this string url)
       where TEntityType : class
        {
            var h = new HttpClient();

            var result = await h.GetStringAsync(new Uri(url, UriKind.Absolute));

            if (string.IsNullOrWhiteSpace(result))
            {
                return null;
            }

            var des = _deserialise<TEntityType>(result);

            return des;
            ;
        }

        public static async Task<string> GetRaw(this string url)
    
        {
            var h = new HttpClient();

            var result = await h.GetStringAsync(new Uri(url, UriKind.Absolute));

            return result;
        }

        private static T _deserialise<T>(string entity) where T : class
        {
            //there is a weird really annoying bug we cannot track that casues json to get an extra } on the end sometimes
            try
            {
                var resultCatch1 = JsonConvert.DeserializeObject<T>(entity);
                return resultCatch1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(entity);
                Debug.WriteLine("JSON Load corrupt: {0}", ex.ToString());
            }

            return null;
        }
    }
}
