using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Task<bool>[] tasks = new Task<bool>[3];
            for(int i=0;i<3;i++)
            {
                tasks[i] = new Task<bool>(
                r =>
                {
                    return sendPromotionItem(r);
                }, i);
                tasks[i].Start();
            }
            try
            {
                Task.WaitAll(tasks);
            }
            catch(AggregateException ex)
            {

            }

            for (int i = 0; i < 3; i++)
            {
                if(tasks[i].Exception!=null && tasks[i].Exception.InnerExceptions!=null && tasks[i].Exception.InnerExceptions.Count>0)
                {
                    Console.WriteLine($"task {i} cause an exception:{string.Join(",", tasks[i].Exception.InnerExceptions.Select(r=>r.Message))}");
                }
                else
                {
                    Console.WriteLine($"task {i} executed success");
                }
            }
            Console.ReadKey();
        }


        public static bool sendPromotionItem(Object obj)
        {
            if((int)obj/2 == 0)
            {
                throw new Exception($"{(int)obj} exception monitor");
            }
            return true;
        }
    }
}
