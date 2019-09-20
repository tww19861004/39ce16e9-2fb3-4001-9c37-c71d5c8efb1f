using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newtonsoftdemo2
{
    public class Parent
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class Child: Parent
    {
        public int Age { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Child c1 = new Child() { id = 1, name = "123", Age = 23 };
            Child c2 = new Child() { id = 2, name = "ssss", Age = 23 };
            List<Parent> lst = new List<Parent>() { c1,c2};
            string s1 = Newtonsoft.Json.JsonConvert.SerializeObject(lst, Formatting.None, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });
            var val =  Newtonsoft.Json.JsonConvert.DeserializeObject<List<Parent>>(s1, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            });
            return;
        }
    }
}
