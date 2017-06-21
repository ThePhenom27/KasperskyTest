using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace KasperskyTest
{
    class TestQueue<T>
    {
        private Queue<T> _queue;
        private ManualResetEvent _mrePush; // Событие о добавлении нового элемента

        public TestQueue()
        {
            _queue = new Queue<T>();
            _mrePush = new ManualResetEvent(false);
        }

        public int Count => _queue.Count;

        public void Push(T item)
        {
            lock (_queue)
            {
                _queue.Enqueue(item);
            }
            _mrePush.Set();
        }

        public T Pop()
        {
            while (true)
            {
                _mrePush.WaitOne();

                lock (_queue)
                {
                    if (_queue.Count == 0) continue; // Итем взял кто-то другой

                    T item = _queue.Dequeue();
                    if (_queue.Count == 0) // Мы взяли последний итем, сбрасываем событие
                        _mrePush.Reset();
                    return item;
                }
            }
        }
    }
}
