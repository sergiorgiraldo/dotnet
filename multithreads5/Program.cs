using System;
using System.Threading;

class Test
{
    static void Main()
    {
        AutoResetEvent[] events = new AutoResetEvent[10];
        for (int i=0; i < events.Length; i++)
        {
            events[i] = new AutoResetEvent(false);
            Runner r = new Runner(events[i], i);
            new Thread(new ThreadStart(r.Run)).Start();
        }
        
        int index = WaitHandle.WaitAny(events);
        
        Console.WriteLine ("***** The winner is {0} *****", 
                           index);

        // if we use ManualResetEvent, we could use waitAll. But autoresetevent need to check each one of the handles
        // WaitHandle.WaitAll(events);

        bool allSet = false;
        while (!allSet)
        {
            for (int i = 0; i < events.Length; i++)
            {
                if (i == index) break; //this was already set by the waitAny
                allSet = events[i].WaitOne();
                if (!allSet) break;
            }
        }

        Console.WriteLine ("All finished!");
    }
}

class Runner
{
    static readonly object rngLock = new object();
    static Random rng = new Random();
    
    AutoResetEvent ev;
    int id;
    
    internal Runner (AutoResetEvent ev, int id)
    {
        this.ev = ev;
        this.id = id;
    }
    
    internal void Run()
    {
        for (int i=0; i < 10; i++)
        {
            int sleepTime;
            // Not sure about the thread safety of Random...
            lock (rngLock)
            {
                sleepTime = rng.Next(2000);
            }
            Thread.Sleep(sleepTime);
            Console.WriteLine ("Runner {0} at stage {1}",
                               id, i);
        }
        Console.WriteLine ("Runner {0} ended", id);
        ev.Set();
    }
}