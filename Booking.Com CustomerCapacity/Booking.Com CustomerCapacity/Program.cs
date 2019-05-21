using System;
using System.Collections.Generic;

namespace Booking.Com_CustomerCapacity
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            TestRunner(Test00, 1);
            TestRunner(Test01, 2);
            TestRunner(Test02, 0);
            TestRunner(Test03, 2);
            TestRunner(Test04, 2);
            TestRunner(Test05, 3);

            Console.WriteLine("Press any key to continue...");

            Console.ReadKey();
        }

        private class Call
        {
            public Call(int start, int end)
            {
                Start = start;
                End = end;
            }

            private int Start { get; }
            private int End { get; }
        }

        private static int NumberOfAgentsToAdd(int executives, int[][] callTimes)
        {
            var set = new SortedList<int, int>();
            foreach (var t in callTimes)
            {
                var beginOfCall = t[0];
                var endOfCall = t[1];

                if (!set.ContainsKey(beginOfCall)) set.Add(beginOfCall, +1); else set[beginOfCall]++;
                if (!set.ContainsKey(endOfCall)) set.Add(endOfCall, -1); else set[endOfCall]--;
            }
            var max = 0;
            var num = 0;
            foreach (var val in set.Values)
            {
                num += val;
                if (max < num) max = num;
            }
            return max - executives;

        }

        private static int Test00()
        {
            const int numOfAgent = 1;
            var callsTimes = new int[3][];

            callsTimes[0] = new int[2];
            callsTimes[0][0] = 1481222000;
            callsTimes[0][1] = 1481222020;
            callsTimes[1] = new int[2];
            callsTimes[1][0] = 1481222000;
            callsTimes[1][1] = 1481222040;
            callsTimes[2] = new int[2];
            callsTimes[2][0] = 1481222030;
            callsTimes[2][1] = 1481222035;

            return NumberOfAgentsToAdd(numOfAgent, callsTimes);
        }

        private static int Test01()
        {
            const int numOfAgent = 1;
            var callsTimes = new int[3][];

            callsTimes[0] = new int[2];
            callsTimes[0][0] = 1481222000;
            callsTimes[0][1] = 1481222020;
            callsTimes[1] = new int[2];
            callsTimes[1][0] = 1481222001;
            callsTimes[1][1] = 1481222040;
            callsTimes[2] = new int[2];
            callsTimes[2][0] = 1481222002;
            callsTimes[2][1] = 1481222035;

            return NumberOfAgentsToAdd(numOfAgent, callsTimes);
        }

        private static int Test02()
        {
            const int numOfAgent = 1;
            var callsTimes = new int[3][];

            callsTimes[0] = new int[2];
            callsTimes[0][0] = 1481222000;
            callsTimes[0][1] = 1481222010;
            callsTimes[1] = new int[2];
            callsTimes[1][0] = 1481222020;
            callsTimes[1][1] = 1481222030;
            callsTimes[2] = new int[2];
            callsTimes[2][0] = 1481222040;
            callsTimes[2][1] = 1481222050;

            return NumberOfAgentsToAdd(numOfAgent, callsTimes);
        }

        private static int Test03()
        {
            const int numOfAgent = 1;
            var callsTimes = new int[3][];

            callsTimes[0] = new int[2];
            callsTimes[0][0] = 1481222000;
            callsTimes[0][1] = 1481222050;
            callsTimes[1] = new int[2];
            callsTimes[1][0] = 1481222020;
            callsTimes[1][1] = 1481222050;
            callsTimes[2] = new int[2];
            callsTimes[2][0] = 1481222040;
            callsTimes[2][1] = 1481222050;

            return NumberOfAgentsToAdd(numOfAgent, callsTimes);
        }

        private static int Test04()
        {
            const int numOfAgent = 1;
            var callsTimes = new int[4][];

            callsTimes[0] = new int[2];
            callsTimes[0][0] = 1481222000;
            callsTimes[0][1] = 1481222050;
            callsTimes[1] = new int[2];
            callsTimes[1][0] = 1481222020;
            callsTimes[1][1] = 1481222050;
            callsTimes[2] = new int[2];
            callsTimes[2][0] = 1481222040;
            callsTimes[2][1] = 1481222050;
            callsTimes[3] = new int[2];
            callsTimes[3][0] = 1481222050;
            callsTimes[3][1] = 1481222060;

            return NumberOfAgentsToAdd(numOfAgent, callsTimes);
        }

        private static int Test05()
        {
            const int numOfAgent = 1;
            var callsTimes = new int[4][];

            callsTimes[0] = new int[2];
            callsTimes[0][0] = 1481222000;
            callsTimes[0][1] = 1481222050;
            callsTimes[1] = new int[2];
            callsTimes[1][0] = 1481222020;
            callsTimes[1][1] = 1481222050;
            callsTimes[2] = new int[2];
            callsTimes[2][0] = 1481222040;
            callsTimes[2][1] = 1481222050;
            callsTimes[3] = new int[2];
            callsTimes[3][0] = 1481222045;
            callsTimes[3][1] = 1481222060;

            return NumberOfAgentsToAdd(numOfAgent, callsTimes);
        }

        private static void TestRunner(Func<int> test, int expected)
        {
            int val;
            Console.WriteLine(
              test.Method.Name + " = {0} -> {1}",
              (val = test.Invoke()), val == expected ? "PASS" : "FAIL");
        }


    }
}
