using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis主从架构
{
    class Program
    {
        static void Main(string[] args)
        {
            //Redis支持主从同步。数据可以从主服务器向任意数量的从服务器上同步，同步使用的是发布/订阅机制。
            //redis提供了一个master,多个slave的服务
            //主IP ：端口      192.168.0.103 6666
            //从IP：端口       192.168.0.108 3333
            //Windows 环境搭建Redis集群
            //Redis集群主从复制（一主两从）
        }
    }
}
