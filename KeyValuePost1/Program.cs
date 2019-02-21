using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace KeyValuePost1
{
    class Program
    {
        static void Main(string[] args)
        {

            var nvc1 = new List<KeyValuePair<string, string>>();
            nvc1.Add(new KeyValuePair<string, string>("uid", "190000000037784266"));
            nvc1.Add(new KeyValuePair<string, string>("gorderId", "3000000088"));

            string str2 = GetKeyValueParmsByObj(new Dictionary<string, string>() { { "uid", "190000000037784266" }, { "gorderId", "3000000088" } });

            //方式1
            string res1 = Post("http://iapi.vip.elong.com/mapi/GetShortLink", new Dictionary<string, string>() { { "uid", "190000000037784266" },{ "gorderId", "3000000088" } });

            using (HttpClient hc12 = new HttpClient())
            {
                //hc.DefaultRequestHeaders.Add("Content-type", "application/x-www-form-urlencoded; charset=utf-8");
                //hc.DefaultRequestHeaders.Add("Connection", "Close");
                var nvc = new List<KeyValuePair<string, string>>();
                nvc.Add(new KeyValuePair<string, string>("uid", "190000000037784266"));
                nvc.Add(new KeyValuePair<string, string>("gorderId", "3000000088"));                

                //方式1
                var client = new HttpClient();
                var req = new HttpRequestMessage(HttpMethod.Post, "xxxxxxxxxxx") { Content = new FormUrlEncodedContent(nvc) };
                var sendTask = hc12.SendAsync(req);
                sendTask.Wait();
                var res = sendTask.Result.Content.ReadAsStringAsync();
                res.Wait();
                JObject jobject = JsonConvert.DeserializeObject<JObject>(res.Result);
                if (jobject != null)
                {
                    if (jobject["msg"]?.ToString() == "success")
                    {
                        string Url = jobject["shortLink"]?.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// 获取键值对组装的请求参数
        /// </summary>
        /// <returns></returns>
        public static string GetKeyValueParmsByObj(dynamic obj)
        {
            PropertyInfo[] propertis = obj.GetType().GetProperties();
            StringBuilder sb = new StringBuilder();
            List<string> parms = new List<string>();
            foreach (var p in propertis)
            {
                var v = p.GetValue(obj, null);
                if (v == null)
                {
                    continue;
                }
                parms.Add(p.Name + "=" + HttpUtility.UrlEncode(v.ToString()));
            }
            return string.Join("&", parms);
        }

        /// <summary>
        /// 指定Post地址使用Get 方式获取全部字符串
        /// </summary>
        /// <param name="url">请求后台地址</param>
        /// <returns></returns>
        public static string Post(string url, Dictionary<string, string> dic)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            #region 添加Post 参数
            StringBuilder builder = new StringBuilder();
            int i = 0;
            foreach (var item in dic)
            {
                if (i > 0)
                    builder.Append("&");
                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }
            byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
    }
}
