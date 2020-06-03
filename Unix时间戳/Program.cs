



namespace Unix时间戳
{
    class Program
    {
        static void Main(string[] args)
        {
            string number = "V6053";
            Regex reg = new Regex("^[vV][0-9]{4,6}$");//必须以V或者v开头，数字4位
            number = "v123";
            Console.WriteLine($"reg.IsMatch({number})={reg.IsMatch(number)}");
            number = "v1234";
            Console.WriteLine($"reg.IsMatch({number})={reg.IsMatch(number)}");
            number = "v12345";
            Console.WriteLine($"reg.IsMatch({number})={reg.IsMatch(number)}");
            number = "v123456";
            Console.WriteLine($"reg.IsMatch({number})={reg.IsMatch(number)}");
            number = "v1234567";
            Console.WriteLine($"reg.IsMatch({number})={reg.IsMatch(number)}");
            if (!reg.IsMatch(number))
            {

            }

            Console.ReadKey();
        }


        /// <summary>  
        /// 获取时间戳  13位
        /// </summary>  
        /// <returns></returns>  
        public static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds * 1000);
        }

        /// <summary>
        /// 将时间戳转换为日期类型，并格式化
        /// </summary>
        /// <param name="longDateTime"></param>
        /// <returns></returns>
        private static string LongDateTimeToDateTimeString(string longDateTime)
        {
            //用来格式化long类型时间的,声明的变量
            long unixDate = long.Parse(longDateTime);
            return LongDateTimeToDateTimeString(unixDate);

        }

        /// <summary>
        /// 将时间戳转换为日期类型，并格式化
        /// </summary>
        /// <param name="longDateTime"></param>
        /// <returns></returns>
        private static string LongDateTimeToDateTimeString(long longDateTime)
        {
            //用来格式化long类型时间的,声明的变量
            long unixDate;
            DateTime start;
            DateTime date;
            //ENd

            unixDate = longDateTime;
            start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            date = start.AddMilliseconds(unixDate).ToLocalTime();

            return date.ToString("yyyy-MM-dd HH:mm:ss");

        }
    }
}
