using ProjektWeb.Data.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektWeb.Services
{
    public interface IElementService
    {
        public Task<List<Element>> GetElements(int? pageNumber);
        public Task<Element> SaveElement(Element element);
    }
}
