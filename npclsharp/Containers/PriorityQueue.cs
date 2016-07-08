using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortableClassLibrary_NP.Containers
{
    
    
    interface IPriorityQueue<T> 
    {
        void Insert(T item, int Priority);
        T Pop();
        bool IsEmpty();
        T Top();

    }
    public class PriorityQueue<T,U> : IPriorityQueue<T>, IEnumerable<T>, IDictionary<T,U>, ICollection<KeyValuePair<T,U>>
    {
        private struct Pair
        {
            public T Value;
            public int Priority;
        }
        private class PriorityQueueEmptyException : Exception { }
        private List<Pair> lista;
        private Comparer<int> comparer;
        public PriorityQueue(OrderType MinimumOrMaximum)
        {
            lista = new List<Pair>();
            comparer = new Comparer<int>(MinimumOrMaximum);
        }
        public void Insert(T item, int Priority)
        {
            lista.Add(new Pair() { Value = item, Priority = Priority });
        }

        public T Pop()
        {
            
            T returnVal = lista[SearchExtremeIndex()].Value;
            lista.RemoveAt(SearchExtremeIndex());
            return returnVal;
        }

        public bool IsEmpty()
        {
            return lista.Count == 0;
        }


        public T Top()
        {
            return lista[SearchExtremeIndex()].Value;
        }


        private int SearchExtremeIndex()
        {
            
            if (IsEmpty()) throw new PriorityQueueEmptyException();
            int ind = 0;
            for (int i = 0; i < lista.Count; i++)
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

        public bool ContainsKey(T key)
        {
            throw new NotImplementedException();
        }

        public ICollection<T> Keys
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(T key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(T key, out U value)
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

        public bool Contains(KeyValuePair<T, U> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<T, U>[] array, int arrayIndex)
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

        public bool Remove(KeyValuePair<T, U> item)
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

        bool ICollection<KeyValuePair<T, U>>.Contains(KeyValuePair<T, U> item)
        {
            throw new NotImplementedException();
        }

        void ICollection<KeyValuePair<T, U>>.CopyTo(KeyValuePair<T, U>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        int ICollection<KeyValuePair<T, U>>.Count
        {
            get { throw new NotImplementedException(); }
        }

        bool ICollection<KeyValuePair<T, U>>.IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        bool ICollection<KeyValuePair<T, U>>.Remove(KeyValuePair<T, U> item)
        {
            throw new NotImplementedException();
        }
    }
}
