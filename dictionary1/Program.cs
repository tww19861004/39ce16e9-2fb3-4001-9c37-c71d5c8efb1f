using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dictionary1
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>()
            {
                { "251019","2"},
                { "251020","3"},
                { "251059","44"},
                { "251021","7"},
            };
            string str1 = Newtonsoft.Json.JsonConvert.SerializeObject(dic);


            string str2 = "{\"251019\":\"2\",\"251020\":\"3\",\"251059\":\"44\",\"251021\":\"7\"}";
            Dictionary<string, string> dic2 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(str2);
        }
    }
}
