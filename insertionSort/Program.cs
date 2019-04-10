using System;

namespace insertionSort
{
    class Program
    {
        static void Main(string[] args)
        {
			 //int[] arr = new int[10] { 23, 9, 85, 12, 99, 34, 60, 15, 100, 1 };
			 int[] arr = new int[5] { 7,10,4,12,15};
			 int n = arr.Length, i, j, val, flag;
			 Console.WriteLine("Insertion Sort");
			 Console.Write("Initial array is: ");   
			 for (i = 0; i < n; i++) {
				Console.Write(arr[i] + " ");
			 }
			 for (i = 1; i < n; i++) {
				val = arr[i];
				flag = 0;
				for (j = i - 1; j >= 0 && flag != 1; ) {
				   if (val < arr[j]) {
					  arr[j + 1] = arr[j];
					  j--;
					  arr[j + 1] = val;
				   }
				   else flag = 1;
				}
			 }
			 Console.Write("\nSorted Array is: ");   
			 for (i = 0; i < n; i++) {
				Console.Write(arr[i] + " ");
			 }
			 Console.ReadLine();             
		}
    }
}
