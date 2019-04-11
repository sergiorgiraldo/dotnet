using System;

namespace Heap101
{
    class Program
    {
        static void Main(string[] args)
        {


            MinHeap mh = new MinHeap(7);
            mh.Insert(8);
            mh.Insert(5);
            mh.Insert(10);
            mh.Insert(3);
            mh.Insert(1);
            mh.Insert(7);
            mh.Insert(20);
            Console.WriteLine(mh.GetMin());
            mh.DisplayHeap();

            mh.RemoveMin();
            Console.WriteLine(mh.GetMin());
            mh.DisplayHeap();

            mh.Remove(7);
            Console.WriteLine(mh.GetMin());
            mh.DisplayHeap();
            int[] arr = { 77, 64, 21, 89, 92, 17, 30, 42, 50, 2 };
            mh.BuildMinHeap(arr);
            mh.DisplayHeap();
        }
    }

    class MinHeap
    {
        int[] arr;
        int arrSize;//size for the array container
        int heapSize;//keeps track of the number of elements
        public MinHeap()
        {
            arrSize = 0;
            heapSize = 0;
            arr = new int[arrSize];
        }

        public MinHeap(int size)
        {
            arr = new int[size];
        }

        public void SetHeapSize(int size)
        {
            arrSize = size;
            arr = new int[size];
        }

        public void Insert(int value)
        {
            if (heapSize == arr.Length)
            {
                throw new Exception("Heap is at full capacity!");
            }

            arr[heapSize] = value;
            heapSize++;
            SiftUp(heapSize - 1);
        }

        public void Remove(int value)
        {
            for (int i = 0; i < heapSize - 1; i++)
            {
                if (arr[i] == value)
                {
                    arr[i] = arr[heapSize - 1];
                    heapSize--;
                    siftDown(i);
                    break;
                }
            }
        }

        public void RemoveMin()
        {
            if (heapSize == 0)
            {
                throw new Exception("Heap is empty!");
            }

            arr[0] = arr[heapSize - 1];
            heapSize--;
            if (heapSize > 0)
            {
                siftDown(0);
            }
        }

        private void SiftUp(int index)
        {
            int parentIndex, temp;
            if (index != 0)
            {
                parentIndex = GetParentIndex(index);
                if (arr[parentIndex] > arr[index])
                {
                    temp = arr[parentIndex];
                    arr[parentIndex] = arr[index];
                    arr[index] = temp;
                    SiftUp(parentIndex);
                }
            }
        }

        private int GetParentIndex(int index)
        {
            return (index - 1) / 2;
        }

        private void siftDown(int nodeIndex)
        {
            int leftChildIndex, rightChildIndex, minIndex, tmp;

            leftChildIndex = GetLeftChildIndex(nodeIndex);

            rightChildIndex = GetRightChildIndex(nodeIndex);

            if (rightChildIndex >= heapSize)
            {
                if (leftChildIndex >= heapSize)
                {
                    return;
                }

                minIndex = leftChildIndex;
            }
            else
            {
                if (arr[leftChildIndex] <= arr[rightChildIndex])
                {
                    minIndex = leftChildIndex;
                }
                else
                {
                    minIndex = rightChildIndex;
                }
            }
            if (arr[nodeIndex] > arr[minIndex])
            {
                tmp = arr[minIndex];

                arr[minIndex] = arr[nodeIndex];

                arr[nodeIndex] = tmp;

                siftDown(minIndex);
            }
        }

        private int GetRightChildIndex(int nodeIndex)
        {
            return (2 * nodeIndex) + 2;
        }

        private int GetLeftChildIndex(int nodeIndex)
        {
            return (2 * nodeIndex) + 1;
        }

        public void DisplayHeap()
        {
            Console.WriteLine("Elements of the heap:");
            for (int i = 0; i < heapSize; i++)
            {
                Console.Write("{0} ", arr[i]);
            }

            Console.WriteLine("\n***********************************");
        }

        public int GetMin()
        {
            return arr[0];
        }

        public void BuildMinHeap(int[] input)
        {
            if (heapSize > 0)
            {
                //clear the current heap
                Array.Resize(ref arr, input.Length);
                heapSize = 0;
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = input[i];
                    heapSize++;
                }
            }
            for (int i = heapSize - 1 / 2; i >= 0; i--)
            {
                MinHeapify(i);
            }
        }

        private void MinHeapify(int index)
        {
            int left = 2 * index;
            int right = (2 * index) + 1;
            int smallest = index;
            if (left < heapSize && arr[left] < arr[index])
            {
                smallest = left;
            }
            else
            {
                smallest = index;
            }
            if (right < heapSize && arr[right] < arr[smallest])
            {
                smallest = right;
            }
            if (smallest != index)
            {
                Swap(ref arr, index, smallest);
                MinHeapify(smallest);
            }
        }

        private void Swap(ref int[] input, int a, int b)
        {
            int temp = input[a];
            input[a] = input[b];
            input[b] = temp;
        }

        public void DeleteHeap()
        {
            Array.Resize(ref arr, 0);
            heapSize = 0;
        }
    }
}
