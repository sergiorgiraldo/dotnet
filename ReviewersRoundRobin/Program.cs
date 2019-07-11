using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ReviewersRoundRobin
{
    class Program
    {
        public static Queue<string> QReviewers = new Queue<string>();
        public static List<string> LReviewers = new List<string>();
        public static string ProjectsFilename = "projects.csv";
        public static string ReviewersFilename = "reviewers.csv";
        public static string LastReviewerFilename = "last.csv";
        public static Dictionary<string, List<string>> Projects = new Dictionary<string, List<string>>();
        public static string LastReviewer;
        public static bool Shuffled;

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: ReviewersRoundRobin <NameOfTheProject>");
                Console.WriteLine("It will return the name of reviewers");

                Environment.Exit(1);
            }

            var project = args[0].Split("|")[0];//"Foo" + DateTime.Now.ToString("HHmmss");
            var author = args[0].Split("|")[1];//"Foo" + DateTime.Now.ToString("HHmmss");

            //read list of reviewers
            using (var sr = new StreamReader(ReviewersFilename))
            {
                var line = sr.ReadToEnd();
                var parts = line.Split(",");
                foreach (var part in parts)
                {
                    LReviewers.Add(part);
                }
            }

            //read list of active projects
            using (var sr = new StreamReader(ProjectsFilename))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    var parts = line.Split(",");
                    var tmpReviewers = new List<string> {parts[1], parts[2], parts[3]};
                    Projects.Add(parts[0], tmpReviewers);
                    line = sr.ReadLine();
                }
            }

            //shuffle if needed
            if ((Projects.Count % Math.Floor(LReviewers.Count / 3.0)) == 0) 
            {
                var rnd = new Random();
                LReviewers = LReviewers.OrderBy(item => rnd.Next()).ToList();

                using (var sw = new StreamWriter(ReviewersFilename))
                {
                    for (int i = 0; i < LReviewers.Count; i++)
                    {
                        sw.Write((i > 0?",":"") + LReviewers[i]);
                    }
                }

                Shuffled = true;
            }

            for (int i = 0; i < LReviewers.Count; i++)
            {
                QReviewers.Enqueue(LReviewers[i]); 
            }

            //if not shuffled, we must continue from the last assigned reviewer
            if (!Shuffled)
            {
                using (var sr = new StreamReader(LastReviewerFilename))
                {
                    LastReviewer = sr.ReadToEnd();
                }

                if (LastReviewer != "")
                {
                    var currentReviewer = QReviewers.Dequeue();
                    while (currentReviewer != LastReviewer)
                    {
                        currentReviewer = QReviewers.Dequeue();
                    }
                }
            }

            QReviewers.TryDequeue(out var reviewer1);
            while (reviewer1 == author) { QReviewers.TryDequeue(out reviewer1);}

            QReviewers.TryDequeue(out var reviewer2);
            while (reviewer2 == author) { QReviewers.TryDequeue(out reviewer2);}

            QReviewers.TryDequeue(out var reviewer3);
            while (reviewer3 == author){QReviewers.TryDequeue(out reviewer3);}

            LastReviewer = reviewer3;

            //save state
            using (var sw = new StreamWriter(LastReviewerFilename, append:false))
            {
                sw.Write(LastReviewer);
            }
            using (var sw = new StreamWriter(ProjectsFilename, append:true))
            {
                sw.WriteLine(project + "," + reviewer1 + "," + reviewer2 + "," + reviewer3);
            }

            Console.WriteLine("Project:" + args[0]);
            Console.WriteLine("Reviewers:");
            Console.WriteLine(reviewer1 + "/" + reviewer2 + "/" + reviewer3);
        }
    }
}