
namespace Multithreads1
{
using System;
using System.Threading;
 
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(new Program().testThread1);
            ThreadPool.QueueUserWorkItem(new Program().testThread2);
 
            Console.ReadLine();
        }
 
        public void testThread1(Object threadContext)
        {
            //executing in thread
            int count = 0;
            while (count++ < 10)
            {
                Console.WriteLine("Thread 1 Executed "+count+" times");
                Thread.Sleep(10);
            }
        }
 
        public void testThread2(Object threadContext)
        {
            //executing in thread
            int count = 0;
            while (count++ < 10)
            {
                Console.WriteLine("Thread 2 Executed " + count + " times");
                Thread.Sleep(18);
            }
        }
    }
}
