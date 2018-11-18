using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireBCL
{
    internal class Node<T> : ICloneable
    {
        public readonly T Element;
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

    public class LightList<T> : IEnumerable<T>, IDisposable where T : IEquatable<T>
    {
        #region Properties and variables
        private Node<T> head;
        private Node<T> tail;

        /// <summary>
        /// Gets the first element of list.
        /// </summary>
        public T First
        {
            get
            {
                return head.Element;
            }
            private set { }
        }

        /// <summary>
        /// Gets the last element of list.
        /// </summary>
        public T Last
        {
            get
            {
                return tail.Element;
            }
            private set { }
        }

        /// <summary>
        /// Gets the count of elements.
        /// </summary>
        public int Count { get; private set; }
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
        }

        /// <summary>
        /// Removes desired element in list.
        /// </summary>
        /// <param name="element">Desired element</param>
        /// <returns>true - if element was removed, false - if wasn't.</returns>
        public bool Remove(T element)
        {
            var temp = head;
            while (true)
            {
                if (temp == null)
                {
                    break;
                }

                if (element.Equals(temp.Element))
                {
                    if (!(temp.PreviousElement is null))
                    {
                        if (temp == head)
                        {
                            head = temp.NextElement;
                        }

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

                        if (temp == tail)
                        {
                            tail = temp.PreviousElement;
                        }

                        temp.NextElement.PreviousElement = temp.PreviousElement;
                    }

                    Count--;

                    return true;
                }

                temp = temp.NextElement;
            }

            return false;
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
        #endregion
    }
}

