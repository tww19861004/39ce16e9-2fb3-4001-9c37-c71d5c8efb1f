using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            //https://stackoverflow.com/questions/31138179/asynchronous-locking-based-on-a-key

            List<Task<string>> list = new List<Task<string>>();
            for (int i = 0; i < 5; i++)
            {
                Task<string> task = new Task<string>(() =>
                {
                    return do1();
                }); task.Start();
                list.Add(task);
            }
            try
            {
                //因为task没有启动所有已知会hold住
                Task.WaitAll(list.ToArray());
            }
            catch { }

            string str = string.Join(",", list.Select(r => r.Result));
        }

        public static string do1()
        {
            CookieContainer cookies1 = new CookieContainer();
            HttpClientHandler handler1 = new HttpClientHandler();
            handler1.CookieContainer = cookies1;

            HttpClient hc1 = new HttpClient(handler1);
            Uri uri = new Uri("http://localhost:8888/api/XsrfToken");
            var res1 = hc1.GetAsync(uri).Result.Content.ReadAsStringAsync().Result;

            IEnumerable<Cookie> responseCookies = cookies1.GetCookies(uri).Cast<Cookie>();
            string cookiename = null;
            string cookievalue = null;
            string domain = null;
            foreach (Cookie cookie in responseCookies)
            {
                cookiename = cookie.Name;
                cookievalue = cookie.Value;
                domain = cookie.Domain;
            }

            JObject jobject1 = JsonConvert.DeserializeObject<JObject>(res1);
            string token = jobject1["token"].ToString();

            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("UserName", "admin"));
            nvc.Add(new KeyValuePair<string, string>("Password", "123456"));
            nvc.Add(new KeyValuePair<string, string>("__RequestVerificationToken", token));


            var handler = new HttpClientHandler() { UseCookies = true };
            handler.CookieContainer = new CookieContainer();
            handler.CookieContainer.Add(new Cookie() { Name = cookiename, Value = cookievalue, Domain = domain });

            using (var client = new HttpClient(handler))
            {
                //client.DefaultRequestHeaders.Clear();
                //client.DefaultRequestHeaders.Add("XSRF-TOKEN", token);
                var req = new HttpRequestMessage(HttpMethod.Post, "http://localhost:9999/Account/Login") { Content = new FormUrlEncodedContent(nvc) };
                var sendTask = client.SendAsync(req);
                sendTask.Wait();
                var res = sendTask.Result.Content.ReadAsStringAsync();
                res.Wait();
                return res.Result;
            }
        }
    }
}
