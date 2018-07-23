using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            MultipleFilesToSingleFile(@"C:\Users\Sergio\Downloads\votacao_partido_munzona_2016", "*.txt", @"C:\Users\Sergio\source\R-Lang\eleicoes2016\votacao.csv");

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        static void MultipleFilesToSingleFile(string dirPath, string filePattern, string destFile)
        {
            string[] arrFiles = Directory.GetFiles(dirPath, filePattern);

            Console.WriteLine("Total File Count : " + arrFiles.Length);

            using (TextWriter tw = new StreamWriter(destFile, true, Encoding.GetEncoding("ISO-8859-1")))
            {
                foreach (string filePath in arrFiles)
                {
                    using (TextReader tr = new StreamReader(filePath, Encoding.GetEncoding("ISO-8859-1")))
                    {
                        tw.WriteLine(tr.ReadToEnd());
                        tr.Close();
                        tr.Dispose();
                    }
                    Console.WriteLine("File Processed : " + filePath);
                }

                tw.Close();
                tw.Dispose();
            }
        }
    }

}
