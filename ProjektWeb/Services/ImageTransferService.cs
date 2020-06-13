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

        async Task<bool> SaveFileOnDisk(IFormFile file)
        {
            try
            {
                using (var fileStream = new FileStream(Path.Combine(DirPath, file.FileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public async Task<bool> SaveFile(IFormCollection httpRequest)
        {
            try
            {
                await SaveFileOnDisk(GetFileFromHeader(httpRequest));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

    }
}
