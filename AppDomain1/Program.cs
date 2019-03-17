using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDomain1
{
    class Program
    {
        static void Main(string[] args)
        {
            //在AppDomain中加载程序集
            var newAppDomain = AppDomain.CreateDomain("NewAppDomain");            

            //建立AssemblyLoad事件处理方法
            newAppDomain.AssemblyLoad +=
                (obj, e) =>
                {
                    Console.WriteLine(string.Format("{0} is loading!", e.LoadedAssembly.GetName()));
                };
            //建立DomainUnload事件处理方法
            newAppDomain.DomainUnload +=
                (obj, e) =>
                {
                    Console.WriteLine("NewAppDomain Unload!");
                };

            newAppDomain.Load("Model");
            foreach (var assembly in newAppDomain.GetAssemblies())
                Console.WriteLine(string.Format("{0}\n----------------------------",
                    assembly.FullName));

            //模拟操作
            for (int n = 0; n < 5; n++)
                Console.WriteLine("  Do Work.......!");
            //卸载AppDomain
            AppDomain.Unload(newAppDomain);

            Console.ReadKey();

            //当加载程序集后，就无法把它从AppDomain中卸载，只能把整个AppDomain卸载
        }
    }
}
