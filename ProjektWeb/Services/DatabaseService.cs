using Microsoft.EntityFrameworkCore;
using ProjektWeb.Data.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ProjektWeb.Services
{
    public class DatabaseService : IDatabaseService
    {
        private DatabaseContext databaseContext;
        public DatabaseService(DatabaseContext context)
        {
            databaseContext = context;
        }

        public List<Element> GetAllElements()
        {
            return databaseContext.Elements.ToList();
        }

        public Element GetElementById(int id)
        {
            return databaseContext.Elements.Where(x => x.ElementId == id).FirstOrDefault();
        }

        public Element GetElementByName(string name)
        {
            return databaseContext.Elements.Where(x => x.Name == name).FirstOrDefault();
        }

        public void AddElement(Element element)
        {
            databaseContext.Elements.Add(element);
            databaseContext.SaveChanges();
        }

        public void AddTagToElementById(int elementId, string tag)
        {
            databaseContext.Tags.Add(new Tag() { ElementId = elementId, Name = tag.ToLower()});
            databaseContext.SaveChanges();
        }

        public IEnumerable<Rate> GetAllRates()
        {
            return databaseContext.Rates.ToList();
        }

        public IEnumerable<Rate> GetRatesByAuthor(string author)
        {
            return databaseContext.Rates.Where(x => x.Author == author).ToList();
        }

        public IEnumerable<Rate> GetRatesByElementId(int elementId)
        {
            return databaseContext.Rates.Where(x => x.ElementId == elementId).ToList();
        }

        public IEnumerable<Tag> GetTagsByElementId(int elementId)
        {
            return databaseContext.Tags.Where(x => x.ElementId == elementId).ToList();
        }

        public IEnumerable<Element> GetElementsContainingTag(string tag)
        {
            List<int> elementIds = databaseContext.Tags.Where(x => x.Name == tag).Select(x => x.ElementId).ToList();
            return databaseContext.Elements.Where(x => elementIds.Contains(x.ElementId)).ToList();
        }

        public IEnumerable<string> GetAllTags()
        {
            List<string> tags = new List<string>();
            databaseContext.Tags.ToList().ForEach(x => tags.Add(x.Name));
            return tags.Distinct();
        }
    }
}
