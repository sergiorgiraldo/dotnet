using System;
using System.Collections;


namespace Stack101
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * Stack stores the values in LIFO (Last in First out) style.
             * The element which is added last will be the element to come out first.
             */
            var myStack = new Stack();
            myStack.Push("Hello!!");
            myStack.Push(null);
            myStack.Push(1);
            myStack.Push(2);
            myStack.Push(3);
            myStack.Push(4);
            myStack.Push(5);

            foreach (var itm in myStack)
                Console.Write(itm);

            Console.WriteLine(myStack.Peek());
            Console.WriteLine(myStack.Peek());
            Console.WriteLine(myStack.Peek());

            myStack.Contains(2); // returns true
            myStack.Contains(10); // returns false

            var myStack2 = new Stack();
            myStack2.Push(1);
            myStack2.Push(2);
            myStack2.Push(3);
            myStack2.Push(4);
            myStack2.Push(5);

            Console.Write("Number of elements in Stack: {0}", myStack2.Count);

            while (myStack2.Count > 0)
                Console.WriteLine(myStack2.Pop());

            Console.Write("Number of elements in Stack: {0}", myStack2.Count);

            Console.ReadLine();
        }
    }
}
