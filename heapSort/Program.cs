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
        public static void Swap(int[] heap, int from, int to){
            int temp = heap[from];
            heap[from] = heap[to];
            heap[to] = temp;
        }

        public static int[] HeapSort(int[] heap)
        {
            int heapSize = heap.Length;
            for (int i = (heapSize - 1) / 2; i >= 0; i--)
            {
                maxHeapify(heap, heapSize, i);
            }

            for (int i = heap.Length - 1; i > 0; i--)
            {
                Swap(heap, i, 0);    
                heapSize--;
                maxHeapify(heap, heapSize, 0);
            }
            return heap;
        }

        public static void maxHeapify(int[] input, int heapSize, int index)
        {
            int left = (index + 1) * 2 - 1;
            int right = (index + 1) * 2;
            int largest = index;
            if (left < heapSize && input[left] > input[index])
            {
                largest = left;
            }

            if (right < heapSize && input[right] > input[largest])
            {
                 largest = right;
            }

            if (largest != index)
            {
                Swap(input, index, largest);

                maxHeapify(input, heapSize, largest);
            }
        }

    }
 }
