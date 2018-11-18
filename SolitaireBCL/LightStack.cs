using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitaireBCL
{
    internal class StackElement<T> : ICloneable
    {
        public readonly T Element;
        public readonly StackElement<T> PreviousElement;
        
        public StackElement(T element)
        {
            if ((dynamic)element is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(element)));
            }

            Element = element;
        }

        public StackElement(T element, StackElement<T> previousElement) : this(element)
        {
            PreviousElement = previousElement;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class LightStack<T> : IEnumerable<T>, IDisposable where T : IEquatable<T>
    {
        #region Properties and variables
        private StackElement<T> current;
        public T Current
        {
            get
            { 
                return current.Element;
            }
            private set { }
        }
        #endregion

        public int Count { get; private set; }

        #region Constructors
        public LightStack()
        {
        }

        public LightStack(T element)
        {
            if ((dynamic)element is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(element)));
            }

            current = new StackElement<T>(element);
        }

        public LightStack(ICollection<T> collection)
        {
            if(collection is null)
            {
                throw new ArgumentNullException(String.Format("{0} is null", nameof(collection)));
            }

            foreach(var element in collection)
            {
                Push(element);
            }
        }
        #endregion

        #region Public methods
        public void Push(T element)
        {
            if (current == null)
            {
                current = new StackElement<T>(element);
            }
            else
            {
                current = new StackElement<T>(element, current);
            }

            Count++;
        }

        public T Pop()
        {
            if (current is null)
            {
                throw new InvalidOperationException();
            }

            T temp = current.Element;
            current = current.PreviousElement;
            Count--;

            return temp;
        }

        public T Peek()
        {
            return current.Element;
        }

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
        #endregion

        #region Implementation of interfaces
        public IEnumerator<T> GetEnumerator()
        {
            StackElement<T> temp = current;
            
            while(true)
            { 
                if (temp is null)
                {
                    yield break;
                }

                yield return temp.Element;
                temp = temp.PreviousElement;
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
