using RedisHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace redis消息队列
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //主要借鉴的方法为ListLeftPop及ListRightPush
            //RedisStore.RedisCache.KeyDelete("201904102002");
            //先进先出
            //一个生产者，多个消费者的情况
            //Redis的列表是使用双向链表实现的，保存了头尾节点，所以在列表头尾两边插取元素都是非常快的

            //https://www.cnblogs.com/stopfalling/p/5375492.html

            //https://www.cnblogs.com/cklovefan/p/7821862.html
        }
    }
}
