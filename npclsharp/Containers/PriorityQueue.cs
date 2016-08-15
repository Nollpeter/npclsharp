using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableClassLibrary_NP.Containers
{
    
    
    interface IPriorityQueue<T> 
    {
        void Insert(T item, Int32 Priority);
        T Pop();
        Boolean IsEmpty();
        T Top();

    }
    public class PriorityQueue<T,U> : IPriorityQueue<T>, IEnumerable<T>, IDictionary<T,U>, ICollection<KeyValuePair<T,U>>
    {
        private struct Pair
        {
            public T Value;
            public Int32 Priority;
        }
        private class PriorityQueueEmptyException : Exception { }
        private List<Pair> lista;
        private Comparer<Int32> comparer;
        public PriorityQueue(OrderType MinimumOrMaximum)
        {
            lista = new List<Pair>();
            comparer = new Comparer<Int32>(MinimumOrMaximum);
        }
        public void Insert(T item, Int32 Priority)
        {
            lista.Add(new Pair() { Value = item, Priority = Priority });
        }

        public T Pop()
        {
            
            T returnVal = lista[SearchExtremeIndex()].Value;
            lista.RemoveAt(SearchExtremeIndex());
            return returnVal;
        }

        public Boolean IsEmpty()
        {
            return lista.Count == 0;
        }


        public T Top()
        {
            return lista[SearchExtremeIndex()].Value;
        }


        private Int32 SearchExtremeIndex()
        {
            
            if (IsEmpty()) throw new PriorityQueueEmptyException();
            Int32 ind = 0;
            for (Int32 i = 0; i < lista.Count; i++)
            {
                if(comparer.Compare(lista[i].Priority, lista[ind].Priority))
                {
                    
                    ind = i;
                }
            }
            return ind;
        }


        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Add(T key, U value)
        {
            throw new NotImplementedException();
        }

        public Boolean ContainsKey(T key)
        {
            throw new NotImplementedException();
        }

        public ICollection<T> Keys
        {
            get { throw new NotImplementedException(); }
        }

        public Boolean Remove(T key)
        {
            throw new NotImplementedException();
        }

        public Boolean TryGetValue(T key, out U value)
        {
            throw new NotImplementedException();
        }

        public ICollection<U> Values
        {
            get { throw new NotImplementedException(); }
        }

        public U this[T key]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(KeyValuePair<T, U> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public Boolean Contains(KeyValuePair<T, U> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<T, U>[] array, Int32 arrayIndex)
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

        public Boolean Remove(KeyValuePair<T, U> item)
        {
            throw new NotImplementedException();
        }

        IEnumerator<KeyValuePair<T, U>> IEnumerable<KeyValuePair<T, U>>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        void ICollection<KeyValuePair<T, U>>.Add(KeyValuePair<T, U> item)
        {
            throw new NotImplementedException();
        }

        void ICollection<KeyValuePair<T, U>>.Clear()
        {
            throw new NotImplementedException();
        }

        Boolean ICollection<KeyValuePair<T, U>>.Contains(KeyValuePair<T, U> item)
        {
            throw new NotImplementedException();
        }

        void ICollection<KeyValuePair<T, U>>.CopyTo(KeyValuePair<T, U>[] array, Int32 arrayIndex)
        {
            throw new NotImplementedException();
        }

        Int32 ICollection<KeyValuePair<T, U>>.Count
        {
            get { throw new NotImplementedException(); }
        }

        Boolean ICollection<KeyValuePair<T, U>>.IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        Boolean ICollection<KeyValuePair<T, U>>.Remove(KeyValuePair<T, U> item)
        {
            throw new NotImplementedException();
        }
    }
}
