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
        private static int _numberOfWords = 6;
        private static int _minLength = 5;
        private static bool _cleanWords = true;
        private static bool _appendDigit = false;
        private static bool _bare = false;
        private static string _separator = "rnd";
        private static bool _multipleSeparators = true;
        private static string _ending = "";
        private static string _wordCase = "L";
        private static readonly List<string> Words = new List<string>();
        private static string _symbols = "!@#$%&*()_+=-[{]};:<>,.?";

        static void Main(string[] args)
        {
            var p = new OptionSet() {
                {
                    "h|?|help", v => ShowHelp()
                },
                {
                    "n|w|words=", v => _numberOfWords = int.Parse(v)
                },
                {
                    "l|len|min=", v => _minLength = int.Parse(v)
                },
                {
                    "c|clean|accents=", v => _cleanWords = (v.ToUpperInvariant() == "Y")
                },
                {
                    "d|digit", v => _appendDigit = true
                },
                {
                    "e|endwith=", v => _ending = v
                },
                {
                    "s|sep|separator=", v => _separator = v
                },
                {
                    "m|multipleSeparators=", v => _multipleSeparators = (v.ToUpperInvariant() == "Y")
                },
                {
                    "case=", v => _wordCase = v.ToUpperInvariant()
                },
                {
                    "b|bare", v => _bare = true
                }
            };
            p.Parse(args);

            using (var sr = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lista.txt")))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    var tokens = line.Split(" ");
                    Words.Add(tokens[1]);
                    line = sr.ReadLine();
                }
            }
            if (_ending.ToLowerInvariant() == "rnd")
            {
                var index = GenerateRandom(0, 23); //23 letter in the alphabet
                _ending = _symbols[index].ToString();
            }

            var passwordChoices = new string[3];
            Output("Here are 3 passwords, pick one:");
            for (var i = 0; i <= 2; i++)
            {
                var password = "";
                for (var j = 0; j < _numberOfWords; j++)
                {
                    var num = GenerateRandom(0, 7775); //7775 words in my dictionary
                    if (Words[num].Length < _minLength)
                    {
                        j -= 1;
                        continue;
                    }

                    var word = (_cleanWords ? RemoveDiacritics(Words[num], true) : Words[num]);
                    switch (_wordCase)
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
                    password += (j > 0 ? GetSeparator() : "") + word;
                }
                if (_appendDigit)
                {
                    var digit = GenerateRandom(1, 99);//numbers from 1 to 99
                    password += GetSeparator() + digit;
                    
                }
                password += _ending;
                Output(password);
                passwordChoices[i] = password;
            }
            Output("---");
            Output(ShowOptions());
            Output("---");
            Output("Your choice (1,2,3)(default: 1)?");

            if (_bare)
            {
                TextCopy.Clipboard.SetText(passwordChoices[0]);
                Output("Copied to clipboard\n", true);
                Output(passwordChoices[0], true);
            }
            else
            {
                var choice = Console.ReadLine();
                if (string.IsNullOrEmpty(choice)) choice = "1";
                if (choice != "1" && choice != "2" && choice != "3")
                {
                    Output("Invalid choice, exiting.");
                }
                else
                {
                    var nChoice = Int32.Parse(choice);
                    nChoice -= 1;
                    TextCopy.Clipboard.SetText(passwordChoices[nChoice]);
                    Output("Option " + choice + " copied to clipboard.");
                }
            }
        }

        private static string GetSeparator()
        {
            var index = GenerateRandom(0, 23);
            if (_separator =="")
            {
                return "";
            }
            else{ 
                if (_separator.ToLowerInvariant() == "rnd" )
                {
                    if (_multipleSeparators)
                    {
                        return _symbols[index].ToString();
                    }
                    else
                    {
                        if (_separator.ToLowerInvariant() == "rnd")
                        {
                            _separator = _symbols[index].ToString();;
                        }
                        return _separator;
                    }
                }
                else{
                    return _separator;
                }
            }
        }

        private static void Output(string what, bool force = false)
        {
            if (!_bare || force) Console.WriteLine(what);
        }

        private static string ShowOptions()
        {
            var options = "These are ";
            options += _numberOfWords + " words, with min length of " + _minLength;
            options += ", formatted as " +
                       (_wordCase == "L" ? "lowercase" : (_wordCase == "T" ? "titlecase" : "uppercase"));
            options += ", separated by \\ " + _separator + " \\, ";
            options += (_appendDigit ? "with" : "without") + " number in the end, ";
            options += "finished with \\ " + _ending + " \\";
            return options;
        }

    //https://web.archive.org/web/20090304194122/http://msdn.microsoft.com:80/en-us/magazine/cc163367.aspx
    public static int GenerateRandom(Int32 minValue, Int32 maxValue)
        {
            var byteArray = new byte[4];

            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException("minValue");
            if (minValue == maxValue) return minValue;
            Int64 diff = maxValue - minValue;
            while (true)
            {
                RandomNumberGenerator.Create().GetBytes(byteArray);
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
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Output("Parameters:");
            Output("\t/(n|w|words)={number}: number of words. default: 6");
            Output("\t/(l|len|min)={number}: minimal length of each word. default: 5");
            Output("\t/(c|clean|accents)={Y|N}: Remove accents. default: Yes");
            Output("\t/case={U|L|T}: Uppercase, Lowercase, Titlecase each word. default: L (lowercase)");
            Output("\t/(e|endwith)={character}: ending character. you must enclose in quotes. if you provide 'rnd', a random symbol will be chosen. default: ' '");
            Output("\t/(s|sep|separator)={character}: separator. you must enclose in quotes. if you provide 'rnd', a random symbol will be chosen. default: 'rnd'");
            Output("\t/(m|multipleSeparators)={Y|N}: Put different separators. Only works with separator is RND. default: Yes");
            Output("\t/(d|digit): Append a random number between 1 and 99. default: No");
            Console.ForegroundColor = currentColor;
            Environment.Exit(0);
        }
    }
}
