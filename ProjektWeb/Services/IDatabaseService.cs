using ProjektWeb.Data.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektWeb.Services
{
    public interface IDatabaseService
    {
        List<Element> GetAllElements();
        Element GetElementById(int id);
        Element GetElementByName(string name);
        IQueryable<Element> GetLazyAllElements();
        IEnumerable<Element> GetElementsContainingTag(string tag);

        IEnumerable<Rate> GetAllRates();
        IEnumerable<Rate> GetRatesByAuthor(string author);

        IEnumerable<string> GetAllTags();


        Task<Element> AddElement(Element element);

        void AddTagToElementById(int elementId, string tag);


        IEnumerable<Rate> GetRatesByElementId(int elementId);

        IEnumerable<Tag> GetTagsByElementId(int elementId);

    }
}
