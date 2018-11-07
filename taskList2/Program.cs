using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace taskList2
{
    public static class monitorsleep
    {
        public static User ToSleep(this User user)
        {
            System.Threading.Thread.Sleep(30000);
            return null;
        }
    }
    public class User
    {
        public string Name { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //task此时还是同步执行的。
            Task<string> task = new Task<string>(
                n =>
                {
                    return ProcessT4(n);
                },new User().ToSleep()
                );
            task.Start();
        }

        private static string ProcessT4(Object obj)
        {
            User user = obj as User;
            return "2";
        }

    }
}
