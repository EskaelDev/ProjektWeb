using ProjektWeb.Data.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektWeb.Services
{
    interface IDatabaseService
    {
        List<Element> GetAllElements();
        Element GetElementById(int id);
        Element GetElementByName(string name);
        IEnumerable<Element> GetElementsContainingTag(string tag);

        IEnumerable<Rate> GetAllRates();
        IEnumerable<Rate> GetRatesByAuthor(string author);

        IEnumerable<string> GetAllTags();
    }
}
