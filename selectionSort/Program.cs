using System;

namespace selectionSort
{
    class Program
    {
        static void printArray(int[] arr, bool skip = true){
            if (skip) Console.WriteLine("");
            for (int i = 0; i < arr.Length; i++) {
                Console.Write(arr[i] + " ");
            }
        } 

        static void Main(string[] args)
        {
            int[] arr = new int[10] { 56, 1, 99, 67, 89, 23, 44, 12, 78, 34 };
            int n = 10;
            Console.WriteLine("Selection sort");  
            Console.Write("Initial array is: ");
            printArray(arr, false);
            int temp, smallest;
            for (int i = 0; i < n - 1; i++) {
                smallest = i;
                for (int j = i + 1; j < n; j++) {
                    if (arr[j] < arr[smallest]) {
                        smallest = j;
                    }
                }
                temp = arr[smallest];
                arr[smallest] = arr[i];
                arr[i] = temp;
                printArray(arr);
            }
            Console.WriteLine();
            Console.Write("Sorted array is: "); 
            printArray(arr, false);
        }
    }
}
