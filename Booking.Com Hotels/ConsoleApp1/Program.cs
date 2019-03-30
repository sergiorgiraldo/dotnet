using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static List<int> Sort_hotels(string keywords, List<int> hotel_ids, List<string> reviews)
        {
            var result = new Dictionary<int, int>();
            var lstKeyword = keywords.Split(' ');

            for (int i = 0; i < reviews.Count; i++)
            {
                var total = Matches(lstKeyword, reviews[i]);
                if (result.ContainsKey(hotel_ids[i]))
                {
                    result[hotel_ids[i]] += total;
                }
                else
                {
                    result.Add(hotel_ids[i], total);
                }
            }
            var sortedResult = result.OrderByDescending(key => key.Value).ToList();
            var lstHotel = new List<int>();
            foreach (var item in sortedResult)
            {
                lstHotel.Add(item.Key);
            }
            return lstHotel;
        }

        static int Matches(string[] lstKeyword, string review)
        {
            var total = 0;
            foreach (var keyword in lstKeyword)
            {
                total += Regex.Matches(review, keyword, RegexOptions.IgnoreCase).Count;
           }
            return total;
        }

        static void Main(string[] args)
        {
            var k = "breakfast beach citycenter location metro view staff price";
            var h = new List<int> { 1, 2, 1, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 1, 2,
                1, 2, 1, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 1, 2,
                1, 2, 1, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 1, 2,
                1, 2, 1, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 1, 2, 1, 2, 1, 1, 2
            };
            var r = new List<string> {
                "This hotel has a nice view of the citycenter. The location is perfect.",
"The breakfast is ok. Regarding location, it is quite far from citycenter but price is cheap so it is worth.",
"Location is excellent, 5 minutes from citycenter. There is also a metro station very close to the hotel.",
"They said I couldn't take my dog and there were other guests with dogs! That is not fair.",
"Very friendly staff and good cost-benefit ratio. Location is a bit far from citycenter.",
"This hotel has a nice view of the citycenter. The location is perfect.",
"The breakfast is ok. Regarding location, it is quite far from citycenter but price is cheap so it is worth.",
"Location is excellent, 5 minutes from citycenter. There is also a metro station very close to the hotel.",
"They said I couldn't take my dog and there were other guests with dogs! That is not fair.",
"Very friendly staff and good cost-benefit ratio. Location is a bit far from citycenter.",
"This hotel has a nice view of the citycenter. The location is perfect.",
"The breakfast is ok. Regarding location, it is quite far from citycenter but price is cheap so it is worth.",
"Location is excellent, 5 minutes from citycenter. There is also a metro station very close to the hotel.",
"They said I couldn't take my dog and there were other guests with dogs! That is not fair.",
"Very friendly staff and good cost-benefit ratio. Location is a bit far from citycenter.",
"This hotel has a nice view of the citycenter. The location is perfect.",
"The breakfast is ok. Regarding location, it is quite far from citycenter but price is cheap so it is worth.",
"Location is excellent, 5 minutes from citycenter. There is also a metro station very close to the hotel.",
"They said I couldn't take my dog and there were other guests with dogs! That is not fair.",
"Very friendly staff and good cost-benefit ratio. Location is a bit far from citycenter.",
"This hotel has a nice view of the citycenter. The location is perfect.",
"The breakfast is ok. Regarding location, it is quite far from citycenter but price is cheap so it is worth.",
"Location is excellent, 5 minutes from citycenter. There is also a metro station very close to the hotel.",
"They said I couldn't take my dog and there were other guests with dogs! That is not fair.",
"Very friendly staff and good cost-benefit ratio. Location is a bit far from citycenter.",
                "This hotel has a nice view of the citycenter. The location is perfect.",
"The breakfast is ok. Regarding location, it is quite far from citycenter but price is cheap so it is worth.",
"Location is excellent, 5 minutes from citycenter. There is also a metro station very close to the hotel.",
"They said I couldn't take my dog and there were other guests with dogs! That is not fair.",
"Very friendly staff and good cost-benefit ratio. Location is a bit far from citycenter.",
"This hotel has a nice view of the citycenter. The location is perfect.",
"The breakfast is ok. Regarding location, it is quite far from citycenter but price is cheap so it is worth.",
"Location is excellent, 5 minutes from citycenter. There is also a metro station very close to the hotel.",
"They said I couldn't take my dog and there were other guests with dogs! That is not fair.",
"Very friendly staff and good cost-benefit ratio. Location is a bit far from citycenter.",
"This hotel has a nice view of the citycenter. The location is perfect.",
"The breakfast is ok. Regarding location, it is quite far from citycenter but price is cheap so it is worth.",
"Location is excellent, 5 minutes from citycenter. There is also a metro station very close to the hotel.",
"They said I couldn't take my dog and there were other guests with dogs! That is not fair.",
"Very friendly staff and good cost-benefit ratio. Location is a bit far from citycenter.",
"This hotel has a nice view of the citycenter. The location is perfect.",
"The breakfast is ok. Regarding location, it is quite far from citycenter but price is cheap so it is worth.",
"Location is excellent, 5 minutes from citycenter. There is also a metro station very close to the hotel.",
"They said I couldn't take my dog and there were other guests with dogs! That is not fair.",
"Very friendly staff and good cost-benefit ratio. Location is a bit far from citycenter.",
"This hotel has a nice view of the citycenter. The location is perfect.",
"The breakfast is ok. Regarding location, it is quite far from citycenter but price is cheap so it is worth.",
"Location is excellent, 5 minutes from citycenter. There is also a metro station very close to the hotel.",
"They said I couldn't take my dog and there were other guests with dogs! That is not fair.",
"Very friendly staff and good cost-benefit ratio. Location is a bit far from citycenter.",
                "This hotel has a nice view of the citycenter. The location is perfect.",
"The breakfast is ok. Regarding location, it is quite far from citycenter but price is cheap so it is worth.",
"Location is excellent, 5 minutes from citycenter. There is also a metro station very close to the hotel.",
"They said I couldn't take my dog and there were other guests with dogs! That is not fair.",
"Very friendly staff and good cost-benefit ratio. Location is a bit far from citycenter.",
"This hotel has a nice view of the citycenter. The location is perfect.",
"The breakfast is ok. Regarding location, it is quite far from citycenter but price is cheap so it is worth.",
"Location is excellent, 5 minutes from citycenter. There is also a metro station very close to the hotel.",
"They said I couldn't take my dog and there were other guests with dogs! That is not fair.",
"Very friendly staff and good cost-benefit ratio. Location is a bit far from citycenter.",
"This hotel has a nice view of the citycenter. The location is perfect.",
"The breakfast is ok. Regarding location, it is quite far from citycenter but price is cheap so it is worth.",
"Location is excellent, 5 minutes from citycenter. There is also a metro station very close to the hotel.",
"They said I couldn't take my dog and there were other guests with dogs! That is not fair.",
"Very friendly staff and good cost-benefit ratio. Location is a bit far from citycenter.",
"This hotel has a nice view of the citycenter. The location is perfect.",
"The breakfast is ok. Regarding location, it is quite far from citycenter but price is cheap so it is worth.",
"Location is excellent, 5 minutes from citycenter. There is also a metro station very close to the hotel.",
"They said I couldn't take my dog and there were other guests with dogs! That is not fair.",
"Very friendly staff and good cost-benefit ratio. Location is a bit far from citycenter.",
"This hotel has a nice view of the citycenter. The location is perfect.",
"The breakfast is ok. Regarding location, it is quite far from citycenter but price is cheap so it is worth.",
"Location is excellent, 5 minutes from citycenter. There is also a metro station very close to the hotel.",
"They said I couldn't take my dog and there were other guests with dogs! That is not fair.",
"Very friendly staff and good cost-benefit ratio. Location is a bit far from citycenter.",
                "This hotel has a nice view of the citycenter. The location is perfect.",
"The breakfast is ok. Regarding location, it is quite far from citycenter but price is cheap so it is worth.",
"Location is excellent, 5 minutes from citycenter. There is also a metro station very close to the hotel.",
"They said I couldn't take my dog and there were other guests with dogs! That is not fair.",
"Very friendly staff and good cost-benefit ratio. Location is a bit far from citycenter.",
"This hotel has a nice view of the citycenter. The location is perfect.",
"The breakfast is ok. Regarding location, it is quite far from citycenter but price is cheap so it is worth.",
"Location is excellent, 5 minutes from citycenter. There is also a metro station very close to the hotel.",
"They said I couldn't take my dog and there were other guests with dogs! That is not fair.",
"Very friendly staff and good cost-benefit ratio. Location is a bit far from citycenter.",
"This hotel has a nice view of the citycenter. The location is perfect.",
"The breakfast is ok. Regarding location, it is quite far from citycenter but price is cheap so it is worth.",
"Location is excellent, 5 minutes from citycenter. There is also a metro station very close to the hotel.",
"They said I couldn't take my dog and there were other guests with dogs! That is not fair.",
"Very friendly staff and good cost-benefit ratio. Location is a bit far from citycenter.",
"This hotel has a nice view of the citycenter. The location is perfect.",
"The breakfast is ok. Regarding location, it is quite far from citycenter but price is cheap so it is worth.",
"Location is excellent, 5 minutes from citycenter. There is also a metro station very close to the hotel.",
"They said I couldn't take my dog and there were other guests with dogs! That is not fair.",
"Very friendly staff and good cost-benefit ratio. Location is a bit far from citycenter.",
"This hotel has a nice view of the citycenter. The location is perfect.",
"The breakfast is ok. Regarding location, it is quite far from citycenter but price is cheap so it is worth.",
"Location is excellent, 5 minutes from citycenter. There is also a metro station very close to the hotel.",
"They said I couldn't take my dog and there were other guests with dogs! That is not fair.",
"Very friendly staff and good cost-benefit ratio. Location is a bit far from citycenter."
            };
            Console.WriteLine(Sort_hotels(k, h, r));
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
