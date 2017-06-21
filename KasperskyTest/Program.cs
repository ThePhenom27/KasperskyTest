using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace KasperskyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test1();
            Test2();
        }

        #region Test 1

        static TestQueue<object> queue;

        static void Test1()
        {
            queue = new TestQueue<object>();

            // Поток который раз в 300 мс выводит число элементов в очереди
            new Thread(QueueMonitor).Start();

            // Потоки, которые параллельно кладут значения
            new Thread(PushThread).Start();
            new Thread(PushThread).Start();

            // Потоки которые достают значения
            new Thread(PopThread).Start();
            new Thread(PopThread).Start();
            new Thread(PopThread).Start();
            new Thread(PopThread).Start();
        }

        static void PushThread()
        {
            while (true)
            {
                queue.Push(new object());
                //Thread.Sleep(1000);
            }
        }

        static void PopThread()
        {
            while (true)
            {
                var obj = queue.Pop();
                //Console.WriteLine("{0} {1}", Thread.CurrentThread.ManagedThreadId, obj.GetHashCode());
            }
        }

        static void QueueMonitor()
        {
            while (true)
            {
                Console.WriteLine(queue.Count);
                Thread.Sleep(300);
            }
        }

        #endregion

        #region Test 2

        static void Test2()
        {
            // Этот метод выводит значения массива, которые в сумме дают заданное число
            SumFind.FindSum(new int[] { 6, 5, 4, 5, 4, 5, 5, 4, 1, 2, 2, 0, 0, 1, 2, 4, 5, 9, 1, 2, 8, 60, -2, 11 }, 9);

            // Этот метод выводит индексы и значения массива, которые в сумме дают заданное число
            SumFind.FindSumUnsorted(new int[] { 6, 5, 4, 5, 4, 5, 5, 4, 1, 2, 2, 0, 0, 1, 2, 4, 5, 9, 1, 2, 8, 60, -2, 11 }, 9);

            Console.ReadKey();
        }

        #endregion
    }
}
