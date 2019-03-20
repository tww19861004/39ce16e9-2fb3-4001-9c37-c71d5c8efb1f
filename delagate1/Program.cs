using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delagate1
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        public static async Task<Response> RunAsync<Request,Response>(Request request, Func<Request, Task<Response>> func)
        {
            try
            {
                return await func(request);
            }
            catch(Exception ex)
            {
                return default(Response);
            }
        }
    }
}
