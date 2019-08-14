using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Interlocked2_Decrement
{    
    //AddRef和Release实现的是一种名为引用计数的内存管理技术，这种技术是使组件能够自己将自己删除的最简单同时也是效率最高的方法。COM组件将维护一个称作是引用计数的数值。
    //当客户虫组件取得一个接口时，此数值增1，当客户使用完某个接口后，此数值将减1。当此数值为0时，组件即可将自己从内存中删除。
　　//为正确的使用引用计数，需要了解一下三条规则：
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
        }

        private static int refCount = 0;
        static void Release()
        {
            long refCount = Interlocked.Decrement(ref refCount);
            if (0 == refCount)
            {
                
            }
        }
    }
}
