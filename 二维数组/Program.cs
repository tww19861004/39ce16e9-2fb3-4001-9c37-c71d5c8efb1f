using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 二维数组
{
    struct OTAInfo
    {
        public int OTAId { get; set; }
        public string OTAName { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string[] OTAGroupList = "2_EAN,13_EAN,27_EAN;11_HOTELBEDS,34_HOTELBEDS;15_AGODA,3_AGODA;1_BOOKING海外,42_BOOKINGCN;17_HOTELSPRO,35_HOTELSPRO".Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            OTAInfo[][] test = new OTAInfo[OTAGroupList.Length][];
            for(int i=0;i < OTAGroupList.Length;i++)
            {
                string[] temp = OTAGroupList[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                test[i] = new OTAInfo[temp.Length];
                for (int j=0;j<temp.Length;j++)
                {
                    OTAInfo ota = new OTAInfo();
                    ota.OTAId = Convert.ToInt32(temp[j].Split('_')[0]);
                    ota.OTAName = temp[j].Split('_')[1];
                    test[i][j] = ota;
                }                
            }
            string test1 = Newtonsoft.Json.JsonConvert.SerializeObject(test);

            OTAInfo[][] test2 = Newtonsoft.Json.JsonConvert.DeserializeObject<OTAInfo[][]>(test1);

            int oatid = 1;
            //找到ota小组
            OTAInfo[] find = test2.FirstOrDefault(r => r.Any(x => x.OTAId == oatid));

        }
    }
}
