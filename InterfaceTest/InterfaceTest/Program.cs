using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceTest
{
    class ITest
    {
        public virtual void Show() { }

        public virtual void Hello()
        {
            Console.WriteLine("Hello From base");
        }

        public virtual void Hi()
        {
            Console.WriteLine("hi from base");
        }
    }

    class test1 : ITest
    {
        public override void Show()
        {
            Console.WriteLine("test one");
        }

        public override void Hello()
        {
            Console.WriteLine("hello test1");
        }
    }

    class test2 : ITest
    {
        public override void Show()
        {
            Console.WriteLine("test two");
        }

        public override void Hi()
        {
            Console.WriteLine("Hi test2");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<ITest> test = new List<ITest>();
            test.Add(new ITest());
            test.Add(new test2());

            test[0].Show();
            test[1].Hi();

            Console.WriteLine(test[1] is test2);
        }
    }
}
