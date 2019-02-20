using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permutations
{
    public class Permutation
    {
        public List<string> Do(List<string> letters)
        {
            var str = letters.Aggregate((i, j) => i + j);
            List<string> output = new List<string>();
            // Working buffer to build new sub-strings
            char[] buffer = new char[str.Length];

            DoRecurse(str.ToCharArray(), 0, buffer, 0, output);

            return output;
        }

        public void DoRecurse(char[] input, int inputPos, char[] buffer, int bufferPos, List<string> output)
        {
            if (inputPos >= input.Length)
            {
                // Add only non-empty strings
                if (bufferPos > 0)
                    output.Add(new string(buffer, 0, bufferPos));

                return;
            }

            // Recurse 2 times - one time without adding current input char, one time with.
            DoRecurse(input, inputPos + 1, buffer, bufferPos, output);

            buffer[bufferPos] = input[inputPos];
            DoRecurse(input, inputPos + 1, buffer, bufferPos + 1, output);
        }
    }
}
