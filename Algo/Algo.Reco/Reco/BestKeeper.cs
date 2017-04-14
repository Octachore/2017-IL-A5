using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo
{
    public class BestKeeper<T>
    {
        private readonly IComparer<T> _comparer;
        private T[] _bestItems;
        private int Capacity => _bestItems.Length;
        private int _count = 0;
        private T Last
        {
            get => _bestItems[Capacity - 1];
            set => _bestItems[Capacity - 1] = value;
        }
        private T First => _bestItems[0];

        public BestKeeper(int capacity, IComparer<T> comparer)
        {
            _bestItems = new T[capacity];
            _comparer = comparer;
        }

        public void Add(T item)
        {
            if (_count < Capacity) _bestItems[_count++] = item;
            else if (_comparer.Compare(Last, item) >= 0) return;
            else AddInArray(item); 
        }

        public T[] GetTop() => _bestItems.Take(_count).OrderByDescending(i=> i, _comparer).ToArray();

        private void AddInArray(T item)
        {
            Last = item;
            _bestItems = _bestItems.OrderByDescending(i => i, _comparer).ToArray();
        }
    }
}
