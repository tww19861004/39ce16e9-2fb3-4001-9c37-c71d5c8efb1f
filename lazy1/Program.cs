using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lazy1
{
    public class Student
    {
        public Student()
        {
            this.Name = "DefaultName";
            this.Age = 0;
            Console.WriteLine("Student is init...");
        }

        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class Test
    {
        public Test()
        {
            Id = Guid.NewGuid();
            //items = new List<string>();
        }
        public Guid Id { get; set; }
        private Lazy<List<string>> _items = new Lazy<List<string>>();
        public List<string> items
        {
            get { return _items.Value; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Lazy<Student> stu = new Lazy<Student>();
            if (!stu.IsValueCreated)
                Console.WriteLine("Student isn't init!");
            Console.WriteLine(stu.Value.Name);
            stu.Value.Name = "Tom";
            stu.Value.Age = 21;
            Console.WriteLine(stu.Value.Name);
            Console.Read();
        }
    }
}
