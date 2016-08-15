using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableClassLibrary_NP.Algorithms.SortingAlgorithms
{
    public class A<T,I> where T:IList<I>
    {
        public A(IList<I> param)
        {

        }
    }
    //public enum OrderType { Ascending, Descending}
    public delegate R GetOrderbyValueDelegate<I,R> (I input);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">The type of the container</typeparam>
    /// <typeparam name="I">The type, which the container consists of </typeparam>
    /// <typeparam name="R">The type, thich you wish to compare by</typeparam>
    public class QuickSort<T,I,R> where T : ICollection<IEnumerable<I>>,IList<I> where I:IComparable<I> where R:IComparable<R>
    {
        
        Int32[] alma = new Int32[10];
        //A<Array<int>,int> alf = new A<Array<int>,int>(new int[10]);
        IList<I> container;
        //A<List<int>> teszt = new A<List<int>>();
        private Comparer<R> comparer;
        private GetOrderbyValueDelegate<I, R> getValue;
        OrderType orderType;
        public QuickSort(IList<I> Container,GetOrderbyValueDelegate<I,R> OrderbyValueDelegate , OrderType OrderType)
        {
            container = Container;
            comparer = new Comparer<R>(orderType);
            orderType = OrderType;
            getValue = OrderbyValueDelegate;
        }
        public QuickSort(IList<I> Container,GetOrderbyValueDelegate<I, R> OrderbyValueDelegate, PortableClassLibrary_NP.ComparingDelegate<R> orderType)
        {
            comparer = new Comparer<R>(orderType);
            getValue = OrderbyValueDelegate;
        }
        public void Sort(T container)
        {
            
        }
        private Int32 Partition(T container, Int32 left,Int32 right)
        {
            I pivot = container[left];
            while(true)
            {
                return 0;
            }
            
        }

    

    }
}
