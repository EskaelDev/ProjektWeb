using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektWeb.Services
{
    public interface IImageTransferService
    {
        public Task<bool> SaveFile(IFormCollection httpRequest);

    }
}
