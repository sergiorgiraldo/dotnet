using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace JumpDos
{
    class Program
    {
        static string jumpsFilename = @"C:\Users\sgiraldo\tools\jumps.txt";
        static Dictionary<int, string> jumps = new Dictionary<int, string>();
        static void Main(string[] args)
        {
            using (StreamReader sr = new StreamReader(jumpsFilename))
            {
                var i = 0;
                var jump = sr.ReadLine();
                while (jump != null)
                {
                    i++;
                    jumps.Add(i, jump);
                    jump = sr.ReadLine();
                }
            }

            if (args.Length == 0 || args[0] == "l")
            {
                foreach (var item in jumps)
                {
                    Console.WriteLine(item.Key + "--" + item.Value);
                }
                Environment.Exit(0);
            }
            if (args[0] == "a")
            {
                using (var sw = new StreamWriter(jumpsFilename))
                {
                    sw.WriteLine(args[1]);
                }
                Environment.Exit(0);
            }

            var newDir = jumps[int.Parse(args[0])];
            Directory.SetCurrentDirectory(newDir);
        }
    }
}
