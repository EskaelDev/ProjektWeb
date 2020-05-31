using Microsoft.EntityFrameworkCore;
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

        public Task<List<Element>> GetElements(int? pageNumber)
        {
            return _databaseService.GetLazyAllElements().Skip(pageNumber.GetValueOrDefault(1) * PageSize).Take(PageSize).ToListAsync();
        }


        public Task<Element> SaveElement(Element newElement)
        {
            return _databaseService.AddElement(newElement);
        }


    }
}
