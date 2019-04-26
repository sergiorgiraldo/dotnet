using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;

namespace ImgUrTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var imgUrImage = Upload(@"C:\Users\sgiraldo\OneDrive - UNIVERSO ONLINE S.A\temp\AlbumArtSmall.jpg");
            while (!imgUrImage.IsCompleted) { }

            Console.ReadLine();
        }

        private static async Task<IImage> Upload(string imgPath)
        {
            var imgUrClient = new ImgurClient("16c839efcf373b3", "9146e6c83a57507b5818e108f78d264dffa06038");
            var endpoint = new ImageEndpoint(imgUrClient);

            var localImg = File.ReadAllBytes(imgPath);

            var img = await endpoint.UploadImageBinaryAsync(localImg);

            return img;
        }
    }
}
