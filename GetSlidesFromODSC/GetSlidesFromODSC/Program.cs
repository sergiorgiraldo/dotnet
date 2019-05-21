using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GetSlidesFromODSC
{
    class Program
    {
        static void Main(string[] args)
        {
            var pattern = new Regex("url=(https.*)", RegexOptions.Multiline);

            DirectoryInfo d = new DirectoryInfo(@"C: \Users\sgiraldo\Downloads\");
            foreach (var file in d.GetFiles())
            {
                using (StreamReader sr = new StreamReader(file.FullName))
                {
                    var contents = sr.ReadToEnd();

                    var matches = pattern.Matches(contents);

                    Console.WriteLine(matches[0].Groups[1].Value.Replace("\">", ""));
                }
            }
            Console.Read();
        }
    }
}
