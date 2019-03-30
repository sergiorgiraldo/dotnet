using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JosephusProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Josephus(5, 2));
            Console.WriteLine("Done");
            Console.ReadLine();
        }

        static int Josephus(int noOfCandidates, int offset)
        {
            Dictionary<int, bool> candidates = new Dictionary<int, bool>();
            for (int i = 0; i < noOfCandidates; i++)
            {
                candidates.Add(i, true);          
            }

            while (candidates.Count(c => c.Value) > 1)
            {
                var currentList = candidates.Where(c => c.Value).ToList();
                var index = 0;
                var found = false;
                while (!found)
                {
                    for (int i = 0; i < currentList.Count; i++)
                    {
                        index += 1;
                        if (index == offset)
                        {
                            candidates[currentList[i].Key] = false;
                            found = true;
                            break;
                        }
                    }
                }
            }

            return candidates.Where(c => c.Value).ToList()[0].Key;
        }
    }
}
