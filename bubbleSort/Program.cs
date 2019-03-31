using System;

namespace bubbleSort
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
            int[] numbers = { 45, 81, 29, 66, 03, 52, 51, 55, 74 }; 
 
            Console.Write("Initial Array: "); 
            printArray(numbers, false);

            bubbleSort(numbers, numbers.Length); 
 
            Console.ReadLine(); 
        } 
 
        static void bubbleSort(int[] arr, int length) 
        { 
            int repos = 0; 
            /* Will go through the vector, comparing each element of the array with the element 
             * immediately following (arr[j] = arr[j + 1];) 
             * The maximum number of implementation of the algorithm for the vector section be  
             * ordained is N - 1, where N is the number of times.  
             */ 
 
            // i determines the number of steps for sorting 
            for (int i = 0; i < length - 1; i++) 
            { 
                // j determines the number of comparisons in each step and the indices to be 
                // studied for comparison 
                for (int j = 0; j < length - (i + 1); j++) 
                {  
                    if (arr[j] > arr[j + 1]) 
                    { 
                        repos = arr[j]; 
                        arr[j] = arr[j + 1]; 
                        arr[j + 1] = repos; 
                    } 
                }

                printArray(arr); 
            } 
        }     
    }
}
