using Microsoft.EntityFrameworkCore;
using ProjektWeb.Controllers;
using ProjektWeb.Data.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektWeb.Services
{
    public class ElementService : IElementService
    {
        public int PageSize => 12;
        private IDatabaseService _databaseService;

        public ElementService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public Task<List<Element>> GetMany(int? pageNumber)
        {
            return _databaseService.GetLazyAllElements().Skip(pageNumber.GetValueOrDefault(0) * PageSize).Take(PageSize).ToListAsync();
        }


        public Task<Element> Create(ElementViewModel newElement)
        {
            if (_databaseService.GetElementByName(newElement.Title) != null)
                return Task.FromResult<Element>(null);

            var element = CreateElementFromElementViewModel(newElement);
            return _databaseService.AddElement(element);
        }

        public async Task<Element> Get(int id)
        {
            return await _databaseService.GetElementById(id).FirstOrDefaultAsync();
        }

        public async Task<bool> Delete(int id)
        {
            return await _databaseService.DeleteElementById(id);
        }
        public async Task<Element> Update(ElementViewModel newElement)
        {
            var element = CreateElementFromElementViewModel(newElement);
            return await _databaseService.UpdateElement(element).FirstOrDefaultAsync();
        }

        private Element CreateElementFromElementViewModel(ElementViewModel newElement)
        {
            return new Element
            {
                Title = newElement.Title,
                Tags = newElement.Tags != null ? newElement.Tags.Select(x => new Tag { Name = x }).ToList() : null,
                ImagePath = null
            };
        }
    }
}
