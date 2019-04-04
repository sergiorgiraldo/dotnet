using System;
using System.Linq;

namespace shellSort
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 23, 9, 85, 12, 99, 34, 60, 15, 100, 1 };
            Console.WriteLine("antes");
            Array.ForEach(array, i => Console.Write(i + " "));

            Console.WriteLine("\ndurante");
            ShellSort(array);

            Console.WriteLine("\ndepois");
            Array.ForEach(array, i => Console.Write(i + " "));
        }

        private static void ShellSort(int[] array)
        {
            int n = array.Length;
            int gap = n / 2;
            int temp;
 
            while (gap > 0)
            {
                for (int i = 0; i + gap < n; i++)
                {
                    int j = i + gap;
                    temp = array[j];
 
                    while (j - gap >= 0 && temp < array[j - gap])
                    {
                        array[j] = array[j - gap];
                        j = j - gap;
                    }
 
                    array[j] = temp;
                    Array.ForEach(array, num => Console.Write(num + " "));
                    Console.WriteLine("");
                }
 
                gap = gap / 2;
            }
        }        
    }
}
