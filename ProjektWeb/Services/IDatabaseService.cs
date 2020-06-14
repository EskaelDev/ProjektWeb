using ProjektWeb.Data.Entities;
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
        IQueryable<Element> GetElementById(int id);
        Element GetElementByName(string name);
        IQueryable<Element> GetLazyAllElements();
        IEnumerable<Element> GetElementsContainingTag(string tag);

        IEnumerable<Rate> GetAllRates();
        IEnumerable<Rate> GetRatesByAuthor(string author);

        IEnumerable<string> GetAllTags();


        Task<Element> AddElement(Element element);
        Task<bool> DeleteElementById(int id);

        void AddTagToElementById(int elementId, string tag);


        IEnumerable<Rate> GetRatesByElementId(int elementId);

        IEnumerable<Tag> GetTagsByElementId(int elementId);

        IQueryable<User> AuthenticateUser(string email, string password);
        IQueryable<User> AddUser(User user);

        IQueryable<User> GetUserById(int id);

        IQueryable<User> GetUserByEmail(string email);

        Task<bool> DeleteUserById(int id);

        IQueryable<Element> UpdateElement(Element element);

        Task<int> GetElementCount();

    }
}
