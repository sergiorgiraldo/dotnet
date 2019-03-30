using System;
using System.Collections.Generic;
using System.Linq;

namespace JosephusProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            Assert(Josephus(5, 1), 4);
            Assert(Josephus(5, 2), 2);
            Assert(Josephus(5, 3), 3);
            Assert(Josephus(10, 3), 3);
            Assert(Josephus(7, 2), 6);
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        static void Assert(int a, int b)
        {
            Console.WriteLine(a + " - " + b + "->" + (a == b?"TRUE":"FALSE"));
        }

        static int Josephus(int noOfCandidates, int skip)
        {
            Dictionary<int, bool> candidates = new Dictionary<int, bool>();
            for (int i = 0; i < noOfCandidates; i++)
            {
                candidates.Add(i, true);          
            }
            var candidateSpot = 0;
            while (candidates.Count(c => c.Value) > 1)
            {
                var currentList = candidates.Where(c => c.Value).ToList();
                var index = 0;
                while (true)
                {
                    index += 1;
                    if (index == skip)
                    {
                        candidates[currentList[candidateSpot].Key] = false;
                        if (candidateSpot == currentList.Count - 1) candidateSpot = 0;
                        break;
                    }
                    else
                    {
                        candidateSpot += 1;
                        if (candidateSpot == currentList.Count) candidateSpot = 0;
                    }
                }
            }

            return candidates.Where(c => c.Value).ToList()[0].Key;
        }
    }
}
