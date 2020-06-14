using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektWeb.Services
{
    public class ImageTransferService : IImageTransferService
    {
        protected string DirPath => Environment.CurrentDirectory + "/wwwroot/elements/images";

        IFormFile GetFileFromHeader(IFormCollection httpRequest)
        {
            IFormFileCollection filesCollection = httpRequest.Files;
            try
            {
                if (filesCollection == null)
                    throw new Exception("No files uploaded");

                var uploadedFile = filesCollection.FirstOrDefault();
                if (uploadedFile == null)
                    throw new Exception("No file was uploaded");
                return
                    uploadedFile;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        async Task<string> SaveFileOnDisk(IFormFile file)
        {
            try
            {
                string path = Path.Combine(DirPath, file.FileName + ".png");
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return Path.Combine("/wwwroot/elements/images", file.FileName + ".png");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task<string> SaveFile(IFormCollection httpRequest)
        {
            try
            {
                return await SaveFileOnDisk(GetFileFromHeader(httpRequest));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

    }
}
