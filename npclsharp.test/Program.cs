using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testWPF;
namespace test
{
    class Program
    {
        class A
        {
            public List<int> lista = new List<int>();
            public readonly int testmember;
            public event EventHandler initialize;
            public A()
            {
                if(initialize != null)
                {
                    initialize(this, new EventArgs());
                }
            }
        }
        public interface ITEstInterface
        {
           void foo();
        }
        public class TestClass : ITEstInterface
        {

            void ITEstInterface.foo()
            {
                throw new NotImplementedException();
               // NotImplementedException e = new NotImplementedException();
                
            }
        }
        static void Main(string[] args)
        {
            //PortableClassLibrary_NP.Containers.PriorityQueue<int> minQ = new PortableClassLibrary_NP.Containers.PriorityQueue<int>(PortableClassLibrary_NP.OrderType.Ascending);
            //minQ.Insert(10, 10);
            PortableClassLibrary_NP.MathLibrary.Function f = new PortableClassLibrary_NP.MathLibrary.Function(function,new PortableClassLibrary_NP.MathLibrary.SetExpression("0","inf",new double[]{}));
            double result = f.Derivative().Derivative().Derivative()[1000];
            Console.WriteLine(result.ToString());
            Console.WriteLine(f.NthDerivative(3)[4].ToString());
            Console.WriteLine(Convert.ToDouble((2/(1000*1000*1000))).ToString());
            Console.WriteLine(f.DefiniteRiemannIntegral(0,10,100000).ToString());
            Console.WriteLine(f.Antiderivative()[100].ToString());
            Console.Read();
            MainWindow mw = new MainWindow();
            mw.Activate();
            mw.Show();
             
        }
        static double function (double x)
        {
            return x*x;
        }
    }
}
