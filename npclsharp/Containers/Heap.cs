using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableClassLibrary_NP.Containers
{
    public interface IHeap<T>
    {

    }
    class Heap<T>: IHeap<T>, ICollection<T> where T:IComparable<T>
    {
        private List<T> innerList;
        Int32 count;
        Comparer<T> comparer;
        ComparingDelegate<T> comparingfunction;

        public Heap(ComparingDelegate<T> OrderingFunction)
        {
            count = 0;
            comparingfunction = OrderingFunction;
            comparer = new Comparer<T>(OrderingFunction);
        }

        private Int32 placeRight(Int32 index)
        {
            return 2 * index + 1;
        }
        private Int32 placeLeft(Int32 index)
        {
            return 2 * index;
        }
        private void repairHeap()
        {
            
        }

        public void Add(T item)
        {
            Int32 ind = 0;
            while(ind < 2*Count+1)
            {
                if (comparer.Compare(comparingfunction, item, innerList[ind])==-1)
                {
                    ind = placeLeft(ind);
                }
                else
                {
                    ind = placeRight(ind);
                }
            }
            innerList[ind] = item;
            //throw new NotImplementedException();

        }

        public void Clear()
        {
            innerList.Clear();
        }

        public Boolean Contains(T item)
        {
            return innerList.Contains(item);
        }

        public void CopyTo(T[] array, Int32 arrayIndex)
        {
            throw new NotImplementedException();
        }

        public Int32 Count
        {
            get { throw new NotImplementedException(); }
        }

        public Boolean IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public Boolean Remove(T item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
