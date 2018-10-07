using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FuncExpression1
{
    class Program
    {
        static void Main(string[] args)
        {            
            Expression<Func<int, int, bool>> funcExpression2 = (n, m) => n - m == 0;            

            //Expression<Func<type,returnType>> = (param) => lamdaexpresion;
            var lstTest = new List<string>();
            //.......业务逻辑
            var lstRes = lstTest.Where(x => x.Contains("_"));

            //初级进化（最原始的匿名委托形式）：
            Func<string, bool> oFunc = delegate (string x) { return x.Contains("_"); };
            lstRes = lstTest.Where(oFunc);

            //高级进化（型如Lamada，但还有匿名委托的影子）：
            Func<string, bool> oFunc1 = (string x) => { return x.Contains("_"); };
            lstRes = lstTest.Where(oFunc1);

            //究极进化（完完全全的Lamada）
            Func<string, bool> oFunc2 = x => x.Contains("_");
            lstRes = lstTest.Where(oFunc2);



        }
    }
}
