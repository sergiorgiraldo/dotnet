using System;
using System.Text.RegularExpressions;

namespace newMeeting
{
    class Program
    {
        static void Main(string[] args)
        {
            
            if (args.Length != 5){
                Console.WriteLine("newMeeting \"<TITLE>\" \"<PEOPLE SEPARATED BY ;>\" \"<PLACE>\" \"<DATETIME>\" \"<SUMMARY>\"");
                Environment.Exit(0);
            }
            else{
                var template = System.IO.File.ReadAllText("template.md");
                var title = args[0];
                var people = args[1].Split(';');
                var place = args[2];
                var dt = args[3];
                var summary = args[4];
                template = template.Replace("{TITLE}", title);
                var people_ = "";
                foreach(var p in people){
                    people_ += "* " + p + Environment.NewLine;
                } 
                template = template.Replace("{PEOPLE}", people_);
                template = template.Replace("{PLACE}", place);
                template = template.Replace("{DATE}", dt);
                template = template.Replace("{SUMMARY}", summary);

                var rootDir  = GetApplicationRoot();
                var wholeDir = System.IO.Path.Combine(rootDir, "out");
                System.IO.Directory.CreateDirectory(wholeDir);
                var newPath = System.IO.Path.Combine(wholeDir, title + ".md");
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
            }
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
