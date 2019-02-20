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
            var result = new List<string>();
            for (int i = 0; i < letters.Count; i++)
            {
                result.Add(letters[i]);
                var permutations = InnerDo(letters[i], i, letters);
                foreach (var item in permutations)
                {
                    if (result.IndexOf(item) == -1)
                    {
                        result.Add(item);
                    }
                }
            }
            return result;
        }

        private List<string> InnerDo(string radix, int curr, List<string> letters)
        {
            var result = new List<string>();

            for (int i = curr + 1; i < letters.Count; i++)
            {
                result.Add(radix + letters[i]);
                result.AddRange(InnerDo(radix + letters[i], i + 1, letters));
            }
            return result;
        }
    }
}
