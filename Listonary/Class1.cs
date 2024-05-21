using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StuffProject.Listonary
{

    /* A Dictionary but you can automatically set the Key to a property such as Name
    "Listonary" Copyright (C) 2024 Jordan Ferrazza

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
    */
    public class Listonary<TKey, TValue> : ICollection<TValue>, IReadOnlyCollection<TValue>, IEnumerable<TValue>
    {
        List<TValue> List = new List<TValue>();

        Func<TValue, TKey> Func;

        /// <summary>
        /// Generates a Listonary
        /// </summary>
        /// <param name="func">The function used to determine a key. It is usually a property value of the object TKey.</param>
        public Listonary(Func<TValue, TKey> func)
        {
            Func = func;

        }

        /// <summary>
        /// Returns an object that holds the key, <c>label</c>.
        /// </summary>
        /// <param name="label">The key to search for.</param>
        /// <returns></returns>
        public TValue this[TKey label] => List.Find(x => Func(x).Equals(label));

        /// <summary>
        /// Generates a collection of the keys.
        /// </summary>
        public TKey[] Keys => List.Select(x => Func(x)).ToArray();
        /// <summary>
        /// Returns a collection of the objects.
        /// </summary>
        public TValue[] Values => List.ToArray();

        /// <summary>
        /// Converts the Listonary to a Dictionary.
        /// </summary>
        /// <returns></returns>
        public Dictionary<TKey, TValue> ToDictionary()
        {
            return List.ToDictionary((x) => Func(x));
        }

        public int Count => List.Count;

        public bool IsReadOnly => false;

        ///<summary>
        /// Adds an item to the Listonary.
        /// </summary>
        ///<exception cref="ArgumentException">Attempt to add a key that was already found in the list.</exception>
        ///<param name="item">The object being addded.</param>
        public void Add(TValue item)
        {
            if (ContainsKey(Func(item))) throw new ArgumentException("The resulting key is already in the list.");
            List.Add(item);
        }

        public void Clear()
        {
            List.Clear();
        }


        public bool Contains(TValue item)
        {
            return List.Contains(item);
        }

        /// <summary>
        /// Determines whether a certain key can be used in the Listonary i.e. the Listonary 'contains' the key.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey item)
        {
            return List.Any((x) => Func(x).Equals(item));
        }

        public void CopyTo(TValue[] array, int arrayIndex)
        {
            List.CopyTo(array, arrayIndex);
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        public bool Remove(TValue item)
        {
            return List.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return List.GetEnumerator();
        }
    }
}
