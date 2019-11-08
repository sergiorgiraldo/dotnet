using System;
using System.IO;
using System.Text.RegularExpressions;
using HumanDateParser;

namespace newMeeting
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Length < 5){                
                Console.WriteLine("newMeeting \"<TITLE>\" \"<PEOPLE SEPARATED BY ;>\" \"<PLACE>\" \"<DATE>\" \"<TIME>\" \"<SUMMARY>\" [\"<NOTES>\"] [/p]");
                Console.WriteLine("\nif (people|title|summary|notes) parameter starts with - (minus), assume it is a path and the file will parsed.");
                Console.WriteLine("Date can be written fluently like in 1 month/after 15 days/tuesday/in 2 weeks. For this, start with -(minus)");
                Console.WriteLine("/p is parameter to print. It must be the last parameter.");
                Console.WriteLine("\n***** REQUIRES mdpdf NPM MODULE");
                Console.WriteLine("\n***** INFO: Internal path:" + GetApplicationRoot());
                Environment.Exit(0);
            }
            else{
                var template = File.ReadAllText(Path.Combine(GetApplicationRoot(), "template.md"));
                var title = Get(args[0]);
                var people = Get(args[1]);
                var place = Get(args[2]);
                var dt = Interpret(args[3]);
                var tm = Get(args[4]);
                var summary = Get(args[5]);
                var notes = "";
                if (args.Length >= 7)
                {
                    if (args[6] != "/p") { 
                        notes = Get(args[6]);
                    }
                }
                template = template.Replace("{TITLE}", title);
                var people_ = "";
                foreach(var p in people.Split(";")){
                    people_ += "* " + p + Environment.NewLine;
                } 
                template = template.Replace("{PEOPLE}", people_);
                template = template.Replace("{PLACE}", place);
                template = template.Replace("{DATE}", dt + " " + tm);
                template = template.Replace("{SUMMARY}", summary);
                template = template.Replace("{NOTES}", notes);

                var rootDir  = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var wholeDir = Path.Combine(rootDir, "out");
                Directory.CreateDirectory(wholeDir);
                var newPath = Path.Combine(wholeDir, dt + "_" +  title.Replace(" ", "_") + ".md");
                File.WriteAllText(newPath, template);
                
                var process = new System.Diagnostics.Process();
                var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                process.StartInfo.FileName = Path.Combine(appDataFolder, @"npm\mdpdf.cmd");
                process.StartInfo.Arguments = newPath;
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();

                var pdfPath = newPath.Replace(".md", ".pdf");

                if (args[args.Length - 1] == "/p") //print
                {
                    process.StartInfo.FileName = "powershell";
                    process.StartInfo.Arguments = " -Command \"Start-Process –FilePath " + pdfPath + " –Verb Print -PassThru | %{ sleep 10;$_} | kill";
                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.CreateNoWindow = false;
                    process.Start();
                    process.WaitForExit();
                }
                process.Dispose();
                File.Delete(newPath);
                Console.WriteLine("Saved at " + pdfPath);
            }
        }

        public static string Get(string what){
            if (what.StartsWith("-"))
                return System.IO.File.ReadAllText(what.Substring(1));
            else    
                return what;
        }
        public static string Interpret(string what){
            if (what.StartsWith("-"))
                return DateParser.Parse(what).ToString("yyyyMMdd");
            else    
                return what;
        }

        public static string GetApplicationRoot()
        {
            var appRoot = Path.GetDirectoryName(Path.GetFullPath(new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).AbsolutePath));
            return appRoot;
        }

    }
}
