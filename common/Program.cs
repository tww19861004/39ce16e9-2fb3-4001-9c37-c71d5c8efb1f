using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public int age { get; set; }
    }
    class Program
    {
        public IQueryable<User> GetAllIncluding(params Expression<Func<User, object>>[] includeProperties)
        {

            IQueryable<User> queryable = null;                            
            //foreach (Expression<Func<User, object>> includeProperty in includeProperties)
            //{

            //    queryable = queryable.Include<User, object>(includeProperty);
            //}

            return queryable;
        }

        static void Main(string[] args)
        {
            var test = new List<User>() { new User() { id = 1,name = "tww"},new User() { id = 2,name="tpp"} };

            var listnew = test.Select(r => new { r.name ,r.id}).ToList();
        }
    }
}
