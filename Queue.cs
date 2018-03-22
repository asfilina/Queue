using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyQueue
{
    public class Queue<T> : System.Collections.Queue where T : class
    {
        public System.Collections.Queue Que { get; set; }
        private readonly object lockOn = new object();
        public Queue()
        {
            Que = new Queue();
        }

        public void Push(T element)
        {
            lock (lockOn)
            {
                Que.Enqueue(element);
                Monitor.Pulse(lockOn);
            }

        }

        public T Pop()
        {
            lock (lockOn)
            {
                while (Que.Count <= 0)
                    Monitor.Wait(lockOn);
                var result = Que.Dequeue() as T;
                return result;
            }
        }
    }
}
