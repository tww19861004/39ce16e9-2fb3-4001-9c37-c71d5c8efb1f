using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace deadlock1
{
    class Program
    {
        public static async Task<JObject> GetJsonAsync(Uri uri)
        {
            using (var client = new HttpClient())
            {
                var jsonString = await client.GetStringAsync(uri).ConfigureAwait(false);
                return JObject.Parse(jsonString);
            }
        }

        static void Main(string[] args)
        {

        }
    }
}
