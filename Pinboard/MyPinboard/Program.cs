using System;
using System.Collections.Generic;
using System.IO;
using pinboard.net;

namespace MyPinboard
{
    class Program
    {
        static void Main(string[] args)
        {
            var onlyTags = false;
            var filter = new List<string>();
            if (args.Length > 0)
            {
                var parameters = args[0];
                if (!string.IsNullOrEmpty(parameters))
                {
                    if (parameters == "-h" || parameters == "/?")
                    {
                        ShowHelp();        
                    }

                    if (parameters == "/tags" || parameters == "/t")
                    {
                        onlyTags = true;
                    }
                    else
                    {
                        filter.AddRange(parameters.Split(","));
                    }
                        
                }
            }
            else{
                ShowHelp();        
            }
            //get your api token from https://pinboard.in/settings/password and store in a file elsewhere
            string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string apiToken = File.ReadAllText(Path.Combine(userPath, "pinboard.key"));

            using (var pb = new PinboardAPI(apiToken))
            {
                if (onlyTags)
                {
                    var tags = pb.Tags.Get().Result;

                    foreach (var tag in tags)
                    {
                        Console.WriteLine(tag.Tag);
                    }
                }
                else
                {
                    var posts = pb.Posts.All(filter).Result;

                    foreach (var post in posts)
                    {
                        Console.WriteLine(post.Description + " -> "+ post.Href);
                    }
                }
            }
        }

        private static void ShowHelp(){
            Console.WriteLine("mypinboard TAGS_SEPARATED_BY_COMMA");
            Console.WriteLine("mypinboard /tags|/t");
            Environment.Exit(0);
        }
    }
}
