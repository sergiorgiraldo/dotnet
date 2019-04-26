using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;

namespace MyStuff.Shared
{
    public class ImgUr
    {
        public static string Upload(byte[] file)
        {
            var imgUr = SendToImgUr(file);
            while (!imgUr.IsCompleted) { }
            return imgUr.Result.Link;
        }

        private static async Task<IImage> SendToImgUr(byte[] file)
        {
            var imgUrClient = new ImgurClient("CLIENT ID", "CLIENT SECRET");
            var imgUrEndpoint = new ImageEndpoint(imgUrClient);

            var imgUr = await imgUrEndpoint.UploadImageBinaryAsync(file);

            return imgUr;
        }
    }
}
