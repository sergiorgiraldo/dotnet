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
            var result = 0;
            while (operators.Count > 0)
            {
                var nFinal1 = numbers.Pop();
                var nFinal2 = numbers.Pop();
                var finalOperator = operators.Pop();
                switch (finalOperator)
                {
                    case "+": result = (nFinal2 + nFinal1); break;
                    case "-": result = (nFinal2 - nFinal1); break;
                    case "*": result = (nFinal2 * nFinal1); break;
                    case "/": result = (nFinal2 / nFinal1); break;
                }
                numbers.Push(result);
            }
            return result;
        }
    }
}
