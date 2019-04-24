using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 二维数组
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] OTAGroupList = "2,13,27;11,34;15,3;1,42;17,35".Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            int[][] test = new int[OTAGroupList.Length][];
            for(int i=0;i < OTAGroupList.Length;i++)
            {
                string[] temp = OTAGroupList[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                test[i] = temp.Select(r => Convert.ToInt32(r)).ToArray();
            }
            string test1 = Newtonsoft.Json.JsonConvert.SerializeObject(test);

            int[][] test2 = Newtonsoft.Json.JsonConvert.DeserializeObject<int[][]>(test1);
        }
    }
}
