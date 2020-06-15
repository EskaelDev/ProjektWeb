using ProjektWeb.Data.Models;
using ProjektWeb.Data.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektWeb.Services
{
    public class RateService : IRateService
    {
        private IDatabaseService _databaseService;
        private IUserService _userService;
        public RateService(IDatabaseService databaseService, IUserService userService)
        {
            _databaseService = databaseService;
            _userService = userService;
        }

        public Task<Rate> Create(RateViewModel newRate)
        {
            Rate checkedRate = GetRateByElementId(newRate.ElementId);
            if (checkedRate != null)
                return Task.FromResult<Rate>(null);

            var rate = new Rate
            {
                Author = _userService.GetCurrentUserId(),
                ElementId = newRate.ElementId,
                Comment = newRate.Comment,
                Score = CheckScore(newRate.Score)
            };
            
            return _databaseService.AddRate(rate);
        }

        public Task<Rate> Delete(int elementId)
        {
            var rate = GetRateByElementId(elementId);
            if (rate == null)
                Task.FromResult<Rate>(null);

            rate.IsDeleted = true;

            return _databaseService.UpdateRate(rate);
        }

        public Task<Rate> Get(int elementId)
        {
            return Task.FromResult(GetRateByElementId(elementId));
        }

        public Task<IEnumerable<Rate>> GetAllRates(int elementId)
        {
            return Task.FromResult(_databaseService.GetRatesByElementId(elementId).Where(x => x.IsDeleted == false));
        }

        public Task<Rate> Update(RateViewModel rateViewModel)
        {
            var rate = GetRateByElementId(rateViewModel.ElementId);
            if (rate == null)
                Task.FromResult<Rate>(null);

            rate.Comment = rateViewModel.Comment;
            rate.Score = CheckScore(rateViewModel.Score);

            return _databaseService.UpdateRate(rate);
        }

        private Rate GetRateByElementId(int elementId)
        {
            return _databaseService.GetRatesByElementId(elementId).Where(x => x.Author == _userService.GetCurrentUserId() && x.IsDeleted == false).FirstOrDefault();
        }

        private int CheckScore(int score)
        {
            if (score > 10) score = 10;
            if (score < 1) score = 1;
            return score;
        }
    }
}
