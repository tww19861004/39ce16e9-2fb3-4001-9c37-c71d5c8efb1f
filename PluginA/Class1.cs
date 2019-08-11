using PluginInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginA
{
    public class PluginA : IPlugin
    {
        public string Show()
        {
            return "插件A";
        }

    }
}
