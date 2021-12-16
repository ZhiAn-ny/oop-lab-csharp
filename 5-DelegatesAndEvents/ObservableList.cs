using System.Linq;

namespace DelegatesAndEvents
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <inheritdoc cref="IObservableList{T}" />
    public class ObservableList<TItem> : IObservableList<TItem>
    {
        private IList<TItem> _list = new List<TItem>();

        /// <inheritdoc cref="IObservableList{T}.ElementInserted" />
        public event ListChangeCallback<TItem> ElementInserted;

        /// <inheritdoc cref="IObservableList{T}.ElementRemoved" />
        public event ListChangeCallback<TItem> ElementRemoved;

        /// <inheritdoc cref="IObservableList{T}.ElementChanged" />
        public event ListElementChangeCallback<TItem> ElementChanged;

        /// <inheritdoc cref="ICollection{T}.Count" />
        public int Count => _list.Count;

        /// <inheritdoc cref="ICollection{T}.IsReadOnly" />
        public bool IsReadOnly => _list.IsReadOnly;

        /// <inheritdoc cref="IList{T}.this" />
        public TItem this[int index]
        {
            get => _list.ElementAt(index);
            set
            {
                var lst = _list.ToArray();
                TItem old = lst[index];
                lst[index] = value;
                _list = new List<TItem>(lst);
                ElementChanged?.Invoke(this, value, old, index);
            }
        }

        /// <inheritdoc cref="IEnumerable{T}.GetEnumerator" />
        public IEnumerator<TItem> GetEnumerator() => _list.GetEnumerator();

        /// <inheritdoc cref="IEnumerable.GetEnumerator" />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <inheritdoc cref="ICollection{T}.Add" />
        public void Add(TItem item)
        {
            _list.Add(item);
            ElementInserted?.Invoke(this, item, _list.Count - 1);
        }

        /// <inheritdoc cref="ICollection{T}.Clear" />
        public void Clear()
        {
            _list.Clear();
        }

        /// <inheritdoc cref="ICollection{T}.Contains" />
        public bool Contains(TItem item)
        {
            return _list.Contains(item);
        }

        /// <inheritdoc cref="ICollection{T}.CopyTo" />
        public void CopyTo(TItem[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc cref="ICollection{T}.Remove" />
        public bool Remove(TItem item)
        {
            ElementRemoved?.Invoke(this, item, _list.IndexOf(item));
            return _list.Remove(item);
        }

        /// <inheritdoc cref="IList{T}.IndexOf" />
        public int IndexOf(TItem item)
        {
            return _list.IndexOf(item);
        }

        /// <inheritdoc cref="IList{T}.RemoveAt" />
        public void Insert(int index, TItem item)
        {
            _list.Insert(index,item);
            ElementInserted?.Invoke(this, item, index);

        }

        /// <inheritdoc cref="IList{T}.RemoveAt" />
        public void RemoveAt(int index)
        {
            var old = _list.ElementAt(index);
            _list.RemoveAt(index);
            ElementRemoved?.Invoke(this, old, index);
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object obj)
        {
            if (obj is ObservableList<TItem>)
                return this.GetHashCode().Equals(obj.GetHashCode());
            return false;
        }

        /// <inheritdoc cref="object.GetHashCode" />
        public override int GetHashCode()
        {
            return _list.GetHashCode();
        }

        /// <inheritdoc cref="object.ToString" />
        public override string ToString()
        {
            return _list.ToString();
        }
    }
}
