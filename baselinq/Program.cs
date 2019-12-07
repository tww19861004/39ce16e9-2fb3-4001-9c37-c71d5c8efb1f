using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace baselinq
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }        
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> test = new List<Person>();
            //where找不到记录 tolist是否有问题
            test.Add(new Person() { Id = 1,Name = "Person1"});
            test.Add(new Person() { Id = 2, Name = "Person2" });
            var t1 = test.Where(r => r.Id > 3).ToList();
            var t2 = test.FindAll(r => r.Id > 3).ToList();
            
        }
    }
}