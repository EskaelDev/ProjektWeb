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


        public async Task<Element> Create(ElementViewModel newElement)
        {
            var element = new Element { Title = newElement.Title , Description = newElement.Description};
            // wyszukanie tagów
            var dbTags = _databaseService.GetAllTags().ToList();

            Dictionary<Tag, int> tagsToAdd = new Dictionary<Tag, int>();
            newElement.Tags.ForEach(tag =>
            {
                var exist = false;
                dbTags.ForEach(dbTag =>
                    {
                        exist = exist || (dbTag.Name == tag);
                        tagsToAdd.Add(dbTag, 0);
                    });
                if (!exist)
                    tagsToAdd.Add(new Tag { Name = tag }, 1);
            });

            foreach (KeyValuePair<Tag, int> tag in tagsToAdd)
            {
                if (tag.Value == 1)
                    await _databaseService.AddTag(tag.Key);
                element.Tags.Add(tag.Key);
            }
                        
            return await _databaseService.AddElement(element);
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
            var element = new Element { Title = newElement.Title };
            return await _databaseService.UpdateElement(element).FirstOrDefaultAsync();
        }
    }
}
