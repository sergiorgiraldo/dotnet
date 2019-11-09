using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;


namespace shorten
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0){
                Console.WriteLine("shorten <url to be shortened>");
                Environment.Exit(0);
            }

            //post to bitly
            var shortenedUrl = Shorten(args[0]).Replace("bit.ly", "s.giral.do");

            //insert into giral.do googl table
            using(MySql.Simple.Database db = ""){
                var sqlStatement = string.Format("INSERT INTO googl (url, short, myName) VALUES ('{0}','{1}','')",args[0], shortenedUrl);
                db.Execute(sqlStatement);

            }

            //output shortened url
            Console.WriteLine(args[0] + "->" + Environment.NewLine + shortenedUrl);
        }

        public static string Shorten(string longUrl)
        {
            //get access token for bity in lastpass
            var url = string.Format("https://api-ssl.bitly.com/v3/shorten?format=json&longUrl={0}&access_token=", longUrl);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);                    
                    JsonSerializer js = new JsonSerializer();
                    dynamic jsonResponse = JsonConvert.DeserializeObject(reader.ReadToEnd());                    
                    return jsonResponse["data"]["url"];
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    using(StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"))){
                        String errorText = reader.ReadToEnd();
                    }
                }
                throw;
            }
        }
    }
}
