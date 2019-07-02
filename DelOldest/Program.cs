using System;
using System.Globalization;
using System.IO;

namespace DelOldest
{
    internal class Program
    {
        // Fields
        public static string[] gArgs;

        // Methods
        public static bool IsNumeric(string words)
        {
            foreach (char ch in words)
            {
                if ((ch < '0') || (ch > '9'))
                {
                    return false;
                }
            }
            return true;
        }

        [STAThread]
        private static void Main(string[] args)
        {
            if ((((args.Length != 5) || !IsNumeric(args[0])) ||
                 ((args[1].ToUpper() != "DAYS") && (args[1].ToUpper() != "MONTHS"))) ||
                ((args[2].ToUpper() != "D+") && (args[2].ToUpper() != "D-") && (args[2].ToUpper() != "DD")) ||
                ((args[3].ToUpper() != "R+") && (args[3].ToUpper() != "R-")))
            {
                Usage();
            }
            else if (!Directory.Exists(Path.GetDirectoryName(args[4])))
            {
                Console.WriteLine("Directory specified does not exist. Please check.\n");
                Usage();
            }
            else
            {
                gArgs = args;
                ProcessDirectory(Path.GetDirectoryName(args[4]), Path.GetFileName(args[4]), args[3].ToUpper() == "R+",
                                 args[2].ToUpper() == "D+", args[2].ToUpper() == "DD");
            }
        }

        public static void ProcessDirectory(string targetDirectory, string fileSpec, bool Recursive, bool DeleteSub,
                                            bool ByDirectoryName)
        {
            if (ByDirectoryName)
            {
                foreach (var directory in Directory.GetDirectories(targetDirectory))
                {
                    DateTime dtDiretorio;
                    if (DateTime.TryParseExact(directory, "yyyyMMdd", CultureInfo.CurrentCulture, DateTimeStyles.None,
                                               out dtDiretorio))
                    {
                        if (VerifyOlder(dtDiretorio))
                            Directory.Delete(directory, true);
                    }
                }
            }
            else
            {
                foreach (string str in Directory.GetFiles(targetDirectory, fileSpec))
                {
                    if (ProcessFile(str))
                    {
                        try
                        {
                            File.Delete(str);
                            Console.WriteLine("Deleting " + str);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine("Unable to delete " + str);
                            Console.WriteLine("\t" + exception.Message);
                        }
                    }
                }
                if (Recursive)
                {
                    foreach (string str2 in Directory.GetDirectories(targetDirectory))
                    {
                        ProcessDirectory(str2, fileSpec, Recursive, DeleteSub, ByDirectoryName);

                        int iFiles = Directory.GetFileSystemEntries(str2).Length;

                        if (iFiles == 0 && DeleteSub)
                            Directory.Delete(str2);
                    }
                }
            }
        }

        public static bool ProcessFile(string fileName)
        {
            DateTime lastWriteTime = File.GetLastWriteTime(fileName);
            return VerifyOlder(lastWriteTime);
        }

        private static bool VerifyOlder(DateTime lastWriteTime)
        {
            switch (gArgs[1].ToUpper())
            {
                case "DAYS":
                    return (lastWriteTime.AddDays(Convert.ToDouble(gArgs[0])) < DateTime.Now);

                case "MONTHS":
                    return (lastWriteTime.AddMonths(Convert.ToInt16(gArgs[0])) < DateTime.Now);
            }
            return false;
        }

        public static void Usage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("DelOldest N UNIT D[+/-/D] R[+/-] FILESPEC");
            Console.WriteLine("D : if recursive, delete empty subdirectory ?; if DD delete based in name of directory (directory name must be in format YYYYMMDD)");
            Console.WriteLine("N : Quantity of units");
            Console.WriteLine("UNITS : can be DAYS OR MONTHS");
            Console.WriteLine("R : Recurse subdirectories ? + for yes, -for no");
            Console.WriteLine(
                @"FILESPEC : Where to delete + file specification. Examples : c:\source\*.bak, c:\temp\*.zip");
        }
    }
}