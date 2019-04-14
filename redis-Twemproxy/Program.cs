using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace redis_Twemproxy
{
    class Program
    {
        static void Main(string[] args)
        {
            //Twemproxy不会增加Redis的性能指标数据，据业界测算，使用twemproxy相比直接使用Redis会带来大约10%的性能下降。
            //但是单个Redis进程的内存管理能力有限。据测算，单个Redis进程内存超过20G之后，效率会急剧下降。
            //目前，建议单个Redis最好配置在8G以内；8G以上的Redis缓存需求，通过Twemproxy来提供支持。

            //182.48.115.236    twemproxy-server    安装nutcracker
            //182.48.115.237    redis - server1       安装redis
            //182.48.115.238    redis - server2       安装redis

            //中间代理层twemproxy需要2台，并且需要结合keepalived（心跳测试）实现高可用，客户端通过vip资源访问twemproxy

        }
    }
}
