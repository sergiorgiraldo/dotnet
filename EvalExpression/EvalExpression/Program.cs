using System;
using System.Collections.Generic;

namespace EvalExpression
{
    class Program
    {
        static void Main(string[] args)
        {
            var exp = "4 - 3 * 12 + 300"; //268
            var result = calc(exp);
            Console.WriteLine(result);

            exp = "7 * 7 + 100 - 12"; //137
            result = calc(exp);
            Console.WriteLine(result);

            exp = "10 - 4"; //6
            result = calc(exp);
            Console.WriteLine(result);

            exp = "8 / 4 + 1"; //3
            result = calc(exp);
            Console.WriteLine(result);

            Console.ReadLine();
        }

        static int calc(string exp)
        {
            Stack<int> numbers = new Stack<int>();
            Stack<string> operators = new Stack<string>();
            List<string> precedence = new List<string> { "*", "/", "+", "-" };

            var tokens = exp.Split(" ");

            for (int i = 0; i < tokens.Length; i++)
            {
                if (i % 2 == 0)
                {
                    numbers.Push(Int32.Parse(tokens[i]));
                }
                else{
                    if (operators.Count > 0)
                    {
                        var currentOperator = operators.Peek();
                        if (precedence.IndexOf(tokens[i]) > precedence.IndexOf(currentOperator))
                        {
                            var n1 = numbers.Pop();
                            var n2 = numbers.Pop();
                            currentOperator = operators.Pop();
                            switch (currentOperator)
                            {
                                case "+": numbers.Push(n2 + n1); break;
                                case "-": numbers.Push(n2 - n1); break;
                                case "*": numbers.Push(n2 * n1); break;
                                case "/": numbers.Push(n2 / n1); break;
                                default: break;
                            }
                        }
                    }
                    operators.Push(tokens[i]);
                }
            }
            Stack<int> numbersRev = new Stack<int>();
            Stack<string> operatorsRev = new Stack<string>();

            //now revert them
            while (numbers.Count != 0)
            {
                numbersRev.Push(numbers.Pop());
            }
            while (operators.Count != 0)
            {
                operatorsRev.Push(operators.Pop());
            }

            var result = 0;
            while (operatorsRev.Count > 0)
            {
                var nFinal1 = numbersRev.Pop();
                var nFinal2 = numbersRev.Pop();
                var finalOperator = operatorsRev.Pop();
                switch (finalOperator)
                {
                    case "+": result = (nFinal1 + nFinal2); break;
                    case "-": result = (nFinal1 - nFinal2); break;
                    case "*": result = (nFinal1 * nFinal2); break;
                    case "/": result = (nFinal1 / nFinal2); break;
                }
                numbersRev.Push(result);
            }
            return result;
        }
    }
}
