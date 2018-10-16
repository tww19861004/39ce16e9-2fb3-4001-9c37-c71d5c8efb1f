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
        //https://stackoverflow.com/questions/28305968/use-task-run-in-synchronous-method-to-avoid-deadlock-waiting-on-async-method
        //https://www.cnblogs.com/OpenCoder/p/4434574.html
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
