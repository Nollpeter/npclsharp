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
        int count;
        Comparer<T> comparer;
        ComparingDelegate<T> comparingfunction;

        public Heap(ComparingDelegate<T> OrderingFunction)
        {
            count = 0;
            comparingfunction = OrderingFunction;
            comparer = new Comparer<T>(OrderingFunction);
        }

        private int placeRight(int index)
        {
            return 2 * index + 1;
        }
        private int placeLeft(int index)
        {
            return 2 * index;
        }
        private void repairHeap()
        {
            
        }

        public void Add(T item)
        {
            int ind = 0;
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

        public bool Contains(T item)
        {
            return innerList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(T item)
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
