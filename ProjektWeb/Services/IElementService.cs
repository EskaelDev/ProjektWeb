using ProjektWeb.Controllers;
using ProjektWeb.Data.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektWeb.Services
{
    public interface IElementService
    {
        public Task<List<Element>> GetMany(int? pageNumber, int? pageSize);
        public Task<Element> Create(ElementViewModel element);
        public Task<Element> Get(int id);

        public Task<bool> Delete(int id);

        public Task<Element> Update(ElementViewModel element);

    }
}
