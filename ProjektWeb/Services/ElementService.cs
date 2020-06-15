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
        private IDatabaseService _databaseService;
 
        public ElementService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
 
        public Task<List<Element>> GetMany(int pageNumber, int pageSize)
        {
            var elemList = _databaseService.GetLazyAllElements().Skip(pageNumber * pageSize).Take(pageSize).ToList();
            elemList.ForEach(elem => _databaseService.GetTagsByElementId(elem.Id).ToList());
            return Task.FromResult(elemList);
        }


        public Task<Element> Create(ElementViewModel newElement)
        {
            if (_databaseService.GetElementByName(newElement.Title) != null)
                return Task.FromResult<Element>(null);

            var element = new Element
            {
                Title = newElement.Title,
                Tags = newElement.Tags != null ? newElement.Tags.Select(x => new Tag { Name = x }).ToList() : null,
                ImagePath = newElement.ImagePath,
                Description = newElement.Description
            };

            return _databaseService.AddElement(element);
        }

        public async Task<Element> Get(int id)
        {
            var element = _databaseService.GetElementById(id).FirstOrDefault();
            if (element != null)
                element.Tags = _databaseService.GetTagsByElementId(element.Id).ToList();
            return element;
        }
        public async Task<int> GetCount()
        {
            return await _databaseService.GetElementCount();
        }

        public async Task<bool> Delete(int id)
        {
            return await _databaseService.DeleteElementById(id);
        }
        public async Task<Element> Update(ElementViewModel newElement)
        {
            var element = _databaseService.GetElementById(newElement.Id).FirstOrDefault();
            element.Description = newElement.Description;
            element.Tags = newElement.Tags != null ? newElement.Tags.Select(x => new Tag { Name = x }).ToList() : null;
            element.ImagePath = newElement.ImagePath;
            return await _databaseService.UpdateElement(element).FirstOrDefaultAsync();
        }
    }
}
