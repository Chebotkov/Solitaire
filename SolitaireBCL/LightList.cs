using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections.Specialized;

namespace SolitaireBCL
{
    internal class Node<T> : ICloneable
    {
        public T Element;
        public Node<T> NextElement;
        public Node<T> PreviousElement;

        public Node(T element)
        {
            if ((dynamic)element is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(element)));
            }

            Element = element;
        }

        public Node(T element, Node<T> previousElement, Node<T> nextElement) : this(element)
        {
            PreviousElement = previousElement;
            NextElement = nextElement;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class LightList<T> : IEnumerable<T>, IDisposable, INotifyPropertyChanged, INotifyCollectionChanged where T : IEquatable<T>
    {
        #region Properties and variables
        private Node<T> head;
        private Node<T> tail;
        private int count;

        /// <summary>
        /// Gets the first element of list.
        /// </summary>
        public T First
        {
            get
            {
                return ((dynamic)head is null) ? (dynamic)null : head.Element;
            }
            private set
            {
                First = value;
                OnPropertyChanged("First");
            }
        }

        /// <summary>
        /// Gets the last element of list.
        /// </summary>
        public T Last
        {
            get
            {
                return ((dynamic)tail is null) ? (dynamic)null : tail.Element;
            }
            private set
            {
                Last = value;
                OnPropertyChanged("Last");
            }
        }

        /// <summary>
        /// Gets the count of elements.
        /// </summary>
        public int Count
        {
            get
            {
                return count;
            }
            private set
            {
                count = value;
                OnPropertyChanged("Count");
            }
        }
        #endregion

        #region Constructors
        public LightList()
        {

        }

        public LightList(IEnumerable<T> collection)
        {
            foreach (T element in collection)
            {
                this.Add(element);
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Adds element to list.
        /// </summary>
        /// <param name="element">Added element.</param>
        public void Add(T element)
        {
            if (head == null)
            {
                head = new Node<T>(element);
                tail = head;
            }
            else
            {
                tail.NextElement = new Node<T>(element);
                var temp = tail;
                tail = tail.NextElement;
                tail.PreviousElement = temp;
            }

            Count++;
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, element));
        }

        /// <summary>
        /// Removes desired element in list.
        /// </summary>
        /// <param name="element">Desired element</param>
        /// <returns>true - if element was removed, false - if wasn't.</returns>
        public bool Remove(T element)
        {
            int index = 0;
            var temp = head;
            while (true)
            {
                if (temp == null)
                {
                    break;
                }

                if (element.Equals(temp.Element))
                {
                    if (temp == head && temp == tail)
                    {
                        head = tail = null;
                    }

                    if (!(temp.PreviousElement is null))
                    {
                        if (temp == tail)
                        {
                            tail = temp.PreviousElement;
                        }

                        temp.PreviousElement.NextElement = temp.NextElement;
                    }

                    if (!(temp.NextElement is null))
                    {
                        if (temp == head)
                        {
                            head = temp.NextElement;
                        }

                        temp.NextElement.PreviousElement = temp.PreviousElement;
                    }

                    Count--;
                    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, element, index));

                    return true;
                }

                temp = temp.NextElement;
                index++;
            }

            return false;
        }

        /// <summary>
        /// Removes element by index.
        /// </summary>
        /// <param name="index">Index of the deleted element</param>
        /// <returns>true - if element was removed, false - if wasn't.</returns>
        public bool RemoveAt(int index)
        {
            var deleted = GetElementByIndex(index);

            return Remove(deleted.Element);
        }

        /// <summary>
        /// Clears list.
        /// </summary>
        public void Clear()
        {
            head = tail = null;
            Count = 0;
            //Dispose
        }

        /// <summary>
        /// Checks if list contains element.
        /// </summary>
        /// <param name="desiredElement">Desired elenment.</param>
        /// <returns>true - if contains, false - if doesn't.</returns>
        public bool Contains(T desiredElement)
        {
            foreach (T element in this)
            {
                if (desiredElement.Equals(element))
                {
                    return true;
                }
            }
            return false;
        }

        public void Reverse()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("List is empty");
            }

            LightList<T> newList = new LightList<T>();
            var temp = tail;
            while (true)
            {
                if (tail == null)
                {
                    break;
                }

                newList.Add(tail.Element);
                tail = tail.PreviousElement;
            }

            this.head = newList.head;
            this.tail = newList.tail;
        }

        public T this[int index]
        {
            get
            {
                if (index > Count - 1)
                {
                    throw new IndexOutOfRangeException();
                }

                return GetElementByIndex(index).Element;
            }
            set
            {
                T oldValue = this[index];
                GetElementByIndex(index).Element = value;
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, oldValue));
            }
        }
        #endregion

        #region Implementation of interfaces
        public IEnumerator<T> GetEnumerator()
        {
            Node<T> temp = head;

            while (true)
            {
                if (temp is null)
                {
                    yield break;
                }

                yield return temp.Element;
                temp = temp.NextElement;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }
        #endregion

        #region Private methods
        private Node<T> GetElementByIndex(int index)
        {
            var temp = head;
            int currentIndex = 0;
            while (currentIndex < index)
            {
                temp = temp.NextElement;
                currentIndex++;
            }

            return temp;
        }
        #endregion
    }
}

