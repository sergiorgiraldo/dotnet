using System;
using System.Text.RegularExpressions;

namespace newMeeting
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 5){
                Console.WriteLine("newMeeting \"<TITLE>\" \"<PEOPLE SEPARATED BY ;>\" \"<PLACE>\" \"<DATE>\" \"<TIME>\" \"<SUMMARY>\" [\"<NOTES>\"]");
                Console.WriteLine("\nif any of the parameters starts with - (minus), assume it is a path and the file will parsed.");
                Console.WriteLine("\n***** REQUIRES mdpdf NPM MODULE*****");
                Environment.Exit(0);
            }
            else{
                var template = System.IO.File.ReadAllText(System.IO.Path.Combine(GetApplicationRoot(), "template.md"));
                var title = Get(args[0]);
                var people = Get(args[1]);
                var place = Get(args[2]);
                var dt = Interpret(args[3]);
                var tm = Get(args[4]);
                var summary = Get(args[5]);                
                var notes = "";
                if (args.Length == 7)
                {
                    notes = Get(args[6]);
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
                var wholeDir = System.IO.Path.Combine(rootDir, "out");
                System.IO.Directory.CreateDirectory(wholeDir);
                var newPath = System.IO.Path.Combine(wholeDir, dt + "_" +  title.Replace(" ", "_") + ".md");
                System.IO.File.WriteAllText(newPath, template);
                
                var process = new System.Diagnostics.Process();
                var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                process.StartInfo.FileName = System.IO.Path.Combine(appDataFolder, @"npm\mdpdf.cmd");
                process.StartInfo.Arguments = newPath;
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();
                process.Dispose();
                System.IO.File.Delete(newPath);
                Console.WriteLine("Saved at " + newPath.Replace(".md", ".pdf"));

            }
        }

        public static string Get(string what){
            if (what.StartsWith("-"))
                return System.IO.File.ReadAllText(what.Substring(1));
            else    
                return what;
        }
        public static string Interpret(string what){
            if (what.ToUpper() == "TODAY")
                return DateTime.Now.ToString("yyyyMMdd");
            else if (what.ToUpper() == "TOMORROW")
                return DateTime.Now.AddDays(1).ToString("yyyyMMdd");
            else if (what.StartsWith("+"))
                return DateTime.Now.AddDays(int.Parse(what.Substring(1))).ToString("yyyyMMdd");
            else    
                return what;
        }

        public static string GetApplicationRoot()
        {
            var exePath = System.IO.Path.GetDirectoryName(System.Reflection
                            .Assembly.GetExecutingAssembly().CodeBase);
            var appPathMatcher=new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return appRoot;
        }

    }
}
