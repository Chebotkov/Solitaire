using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

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

    public class LightStack<T> : IEnumerable<T>, IDisposable, INotifyCollectionChanged where T : IEquatable<T>
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

        public int Count { get; private set; }
        #endregion

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

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, element));
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

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, temp));
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

        public void Reverse()
        {
            current = GetReversedVersion().current;
        }

        public LightStack<T> GetReversedVersion()
        {
            LightStack<T> newStack = new LightStack<T>();
            foreach (T element in this)
            {
                newStack.Push(element);
            }

            return newStack;
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

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }
        #endregion
    }
}
