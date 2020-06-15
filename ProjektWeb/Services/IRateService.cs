using ProjektWeb.Data.Models;
using ProjektWeb.Data.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektWeb.Services
{
    public interface IRateService
    {
        public Task<Rate> Create(RateViewModel rateViewModel);
        public Task<Rate> Update(RateViewModel rateViewModel);
        public Task<Rate> Delete(int elementId);
        public Task<Rate> Get(int elementId);
        public Task<IEnumerable<Rate>> GetAllRates(int elementId);
    }
}
