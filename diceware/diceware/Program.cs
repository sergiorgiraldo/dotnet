using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using NDesk.Options;

namespace diceware
{
    class Program
    {
        private static int _numberOfWords = 4;
        private static int _minLength = 5;
        private static bool _cleanWords = true;
        private static string _wordcase = "L";
        private static string _separator = "*";
        private static readonly List<string> words = new List<string>();

        static void Main(string[] args)
        {
            var p = new OptionSet() {
                {
                    "h|?|help", v => ShowHelp ()
                },
                {
                    "n|w|words=", v => _numberOfWords = int.Parse(v)
                },
                {
                    "l|len|min=", v => _minLength = int.Parse(v)
                },
                {
                    "c|clean|accents=", v => _cleanWords = (v == "Y")
                },
                {
                    "s|sep|separator=", v => _separator = v
                },
                {
                    "case=", v => _wordcase = v.ToUpperInvariant()
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

            Console.WriteLine("Here are 3 passwords, pick one:");
            for (var i = 0; i <= 2; i++)
            {
                var password = "";
                for (var j = 0; j < _numberOfWords; j++)
                {
                    var num = GenerateRandom(0, 7775);
                    if (words[num].Length < _minLength)
                    {
                        j -= 1;
                        continue;
                    }

                    var word = (_cleanWords ? RemoveDiacritics(words[num], true) : words[num]);
                    switch (_wordcase)
                    {
                        case "U":
                            word = word.ToUpperInvariant();
                            break;
                        case "T":
                            var textInfo = new CultureInfo("pt-br", false).TextInfo;
                            word = textInfo.ToTitleCase(word); 
                            break;
                        default:
                            word = word.ToLowerInvariant();
                            break;
                    }
                    password += (j > 0 ? _separator : "") + word;
                }
                Console.WriteLine("\t" + password);
            }
            Console.WriteLine("---");
            Console.WriteLine(ShowOptions());

        }

        private static string ShowOptions()
        {
            var options = "These are ";
            options += _numberOfWords + " words, with min length of " + _minLength;
            options += ", formatted as " +
                       (_wordcase == "L" ? "lowercase" : (_wordcase == "T" ? "titlecase" : "uppercase"));
            options += ", separated by \\ " + _separator + " \\.";
            return options;
        }

    //https://web.archive.org/web/20090304194122/http://msdn.microsoft.com:80/en-us/magazine/cc163367.aspx
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
        //end

        //https://stackoverflow.com/questions/3769457/how-can-i-remove-accents-on-a-string
        public static IEnumerable<char> RemoveDiacriticsEnum(string src, bool compatNorm, Func<char, char> customFolding)
        {
            foreach (char c in src.Normalize(compatNorm ? NormalizationForm.FormKD : NormalizationForm.FormD))
                switch (CharUnicodeInfo.GetUnicodeCategory(c))
                {
                    case UnicodeCategory.NonSpacingMark:
                    case UnicodeCategory.SpacingCombiningMark:
                    case UnicodeCategory.EnclosingMark:
                        //do nothing
                        break;
                    default:
                        yield return customFolding(c);
                        break;
                }
        }

        public static string RemoveDiacritics(string src, bool compatNorm, Func<char, char> customFolding)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in RemoveDiacriticsEnum(src, compatNorm, customFolding))
                sb.Append(c);
            return sb.ToString();
        }

        public static string RemoveDiacritics(string src, bool compatNorm)
        {
            return RemoveDiacritics(src, compatNorm, c => c);
        }
        //end
        private static void ShowHelp()
        {
            Console.WriteLine("Parameters:");
            Console.WriteLine("/(n|w|words)={number}: number of words. default: 4");
            Console.WriteLine("/(l|len|min)={number}: minimal length of each word. default: 5");
            Console.WriteLine("/(c|clean|accents)={Y|N}: Remove accents. default: Y");
            Console.WriteLine("/case={U|L|T}: Uppercase, Lowercase, Titlecase each word. default: L (lowercase)");
            Console.WriteLine("/(s|sep|separator)={character}: separator. default: -");

            Environment.Exit(0);
        }
    }
}
