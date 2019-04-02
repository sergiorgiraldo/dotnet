using System;

namespace heapSort
{
 class Program
    {
        static void Main(string[] args)
        {
            ///
            /// Heap Sort Algorithm sample
            ///
            Console.WriteLine("\n");
            int[] testheap = { 10, 64, 7, 99, 32, 18, 2, 48 };

            Console.WriteLine("Given test heap as input :");         
            Console.WriteLine(string.Join(",",testheap)); 

            int[] sortedheap = sampleHeapSort.HeapSort(testheap);

            Console.WriteLine("Sorted heap as output :");
            Console.WriteLine(string.Join(",",sortedheap)); 
        }
    }
 public class sampleHeapSort
    {
        public static int[] HeapSort(int[] heap)
        {
            int heapSize = heap.Length;
            for (int i = (heapSize - 1) / 2; i >= 0; i--)
            {
                maxHeapify(heap, heapSize, i);
            }

            for (int i = heap.Length - 1; i > 0; i--)
            {
                int temp = heap[i];
                heap[i] = heap[0];
                heap[0] = temp;

                heapSize--;
                maxHeapify(heap, heapSize, 0);
            }
            return heap;
        }

        public static void maxHeapify(int[] input, int heapSize, int index)
        {
            int left = (index + 1) * 2 - 1;
            int right = (index + 1) * 2;
            int largest = 0;
            if (left < heapSize && input[left] > input[index])
            {
                largest = left;
            }
            else {
                largest = index;
            }

            if (right < heapSize && input[right] > input[largest])
            {
                 largest = right;
            }

            if (largest != index)
            {
                int temp = input[index];
                input[index] = input[largest];
                input[largest] = temp;

                maxHeapify(input, heapSize, largest);
            }
        }

    }
 }
