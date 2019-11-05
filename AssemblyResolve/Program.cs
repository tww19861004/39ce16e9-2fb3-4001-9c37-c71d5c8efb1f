using ClassLibrary1;
using System;
using System.Reflection;

namespace AssemblyResolve
{
    //https://segmentfault.com/a/1190000000515580
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args1) =>
            {
                String resourceName = "AssemblyResolve." +
                   new AssemblyName(args1.Name).Name + ".dll";
                Console.WriteLine(resourceName);
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    Byte[] assemblyData = new Byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            };
            Test();
            Console.ReadKey();
        }

        static void Test()
        {
            //class1 为另外一个dll里的类
            Class1 c1 = new Class1();
            Console.WriteLine(c1.ToString());
        }
    }
}
