using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MvcAjax.Models
{
    public class PeopleRepository : IList<Person>
    {
        private readonly IList<Person> _items;
        private static readonly object _lock = new object();
        private static PeopleRepository _instance = null;

        private PeopleRepository()
        {
            _items = new List<Person>();
            Add(new Person {Name = "John", Surname = "Doe", Email = "john@example.com", Age = 43});
            Add(new Person {Name = "Gertha", Surname = "Reichel", Age = 35});
            Add(new Person {Name = "Mister", Surname = "Muscle", Age = 55});
        }

        public static PeopleRepository Current
        {
            get
            {
                if (_instance == null) {
                    lock (_lock) {
                        if (_instance == null) {
                            _instance = new PeopleRepository();
                        }
                    }
                }
                return _instance;
            }
        }

        public IEnumerator<Person> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Person item)
        {
            InvalidateId(item);
            _items.Add(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(Person item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(Person[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public bool Remove(Person item)
        {
            return _items.Remove(item);
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public int IndexOf(Person item)
        {
            return _items.IndexOf(item);
        }

        public void Insert(int index, Person item)
        {
            InvalidateId(item);
            _items.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
        }

        public Person this[int index]
        {
            get { return _items[index]; }
            set
            {
                InvalidateId(value);
                _items[index] = value;
            }
        }

        private void InvalidateId(Person newItem)
        {
            if (newItem.Id == 0) {
                if (!_items.Any()) {
                    newItem.Id = 1;
                }
                else {
                    newItem.Id = _items.Max(x => x.Id) + 1;
                }
            }
            else if (_items.Any(x => x.Id == newItem.Id)) {
                throw new ArgumentException("An item with the same key has already been added.");
            }
        }

    }
}