using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using NDesk.Options;

namespace diceware
{
    class Program
    {
        private static int numberOfWords = 5;
        private static int minLength = 3;
        private static bool cleanWords = false;
        private static string separator = "-";
        private static List<string> words = new List<string>();

        static void Main(string[] args)
        {
            var p = new OptionSet () {
                {
                    "h|?|help", v => ShowHelp ()
                },
                {
                    "n|w|words", v => numberOfWords = int.Parse(v)
                },
                {
                    "l|len|min", v => minLength = int.Parse(v)
                },
                {
                    "c|clean|accents", v => cleanWords = bool.Parse(v)
                },
                {
                    "s|sep|separator", v => separator = v
                }
            };
            p.Parse(args);

            using (var sr = new StreamReader("lista.txt"))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    var tokens = line.Split(" ");
                    words.Add(tokens[1]);
                    line = sr.ReadLine();
                }
            }

            Console.WriteLine("Here are 3 options:");
            for (var i = 0; i <= 2; i++)
            {
                var password = "";
                for (var j = 0; j < numberOfWords; j++)
                {
                    var num = GenerateRandom(0, 7775);
                    if (words[num].Length < minLength)
                    {
                        j -= 1;
                        continue;
                    }
                    password += (j > 0 ? separator : "") + words[num];
                }
                Console.WriteLine("\t" + password);
            }
        }

        public static int GenerateRandom(Int32 minValue, Int32 maxValue)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            var byteArray = new byte[4];

            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException("minValue");
            if (minValue == maxValue) return minValue;
            Int64 diff = maxValue - minValue;
            while (true)
            {
                provider.GetBytes(byteArray);
                UInt32 rand = BitConverter.ToUInt32(byteArray, 0);

                Int64 max = (1 + (Int64)UInt32.MaxValue);
                Int64 remainder = max % diff;
                if (rand < max - remainder)
                {
                    return (Int32)(minValue + (rand % diff));
                }
            }
        }
        private static void ShowHelp()
        {
            Console.WriteLine("Parameters:");
            Console.WriteLine("n|w|words: number of words. default: 5");
            Console.WriteLine("l|len|min: minimal length of each word. default: 3");
            Console.WriteLine("c|clean|accents: remove accents. default: false");
            Console.WriteLine("s|sep|separator: separator. default: -");

            Environment.Exit(0);
        }
    }
}
