using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Com_CustomerCapacity
{
    class Program
    {
        static void Main(string[] args)
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

        class Call
        {
            public Call(int start, int end)
            {
                Start = start;
                End = end;
            }

            public int Start { get; set; }
            public int End { get; set; }
        }

        static int numberOfAgentsToAdd(int executives, int[][] callTimes)
        {
            var set = new SortedList<int, int>();
            for (int i = 0; i < callTimes.Length; ++i)
            {
                var b = callTimes[i][0];
                var e = callTimes[i][1];

                if (!set.ContainsKey(b)) set.Add(b, +1); else set[b]++;
                if (!set.ContainsKey(e)) set.Add(e, -1); else set[e]--;
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

        static int Test00()
        {
            var numOfAgent = 1;
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

            return numberOfAgentsToAdd(numOfAgent, callsTimes);
        }

        static int Test01()
        {
            var numOfAgent = 1;
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

            return numberOfAgentsToAdd(numOfAgent, callsTimes);
        }

        static int Test02()
        {
            var numOfAgent = 1;
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

            return numberOfAgentsToAdd(numOfAgent, callsTimes);
        }

        static int Test03()
        {
            var numOfAgent = 1;
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

            return numberOfAgentsToAdd(numOfAgent, callsTimes);
        }

        static int Test04()
        {
            var numOfAgent = 1;
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

            return numberOfAgentsToAdd(numOfAgent, callsTimes);
        }

        static int Test05()
        {
            var numOfAgent = 1;
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

            return numberOfAgentsToAdd(numOfAgent, callsTimes);
        }

        static void TestRunner(Func<int> test, int expected)
        {
            int val = 0;
            Console.WriteLine(
              test.Method.Name + " = {0} -> {1}",
              (val = test.Invoke()), val == expected ? "PASS" : "FAIL");
        }


    }
}
