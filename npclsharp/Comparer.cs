using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableClassLibrary_NP
{
    public enum OrderType { Ascending, Descending }
    public delegate Int32 ComparingDelegate<T>(T first, T second);
    public class Comparer<T> where T : IComparable<T>
    {
        
        ComparingDelegate<T> compareFunction;
        OrderType relation;
        public Comparer(OrderType GreaterOrLess)
        {
            relation = GreaterOrLess;
        }
        public Comparer(ComparingDelegate<T> function)
        {
            compareFunction = function;
        }
        public Boolean Compare(T first, T second)
        {
            switch (relation)
            {
                case OrderType.Ascending:
                    {
                        return first.CompareTo(second) < 0;
                    }
                default:
                    {
                        return first.CompareTo(second) > 0;
                    }
            }
        }
        public Int32 Compare(ComparingDelegate<T> function, T first, T second)
        {
            return function(first, second);
        }
    }
}
