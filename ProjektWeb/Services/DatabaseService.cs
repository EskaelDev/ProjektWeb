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

        public IEnumerable<Rate> GetAllRates()
        {
            return databaseContext.Rates.ToList();
        }

        public IEnumerable<Rate> GetRatesByAuthor(string author)
        {
            return databaseContext.Rates.Where(x => x.Author == author).ToList();
        }

        public IEnumerable<Element> GetElementsContainingTag(string tag)
        {
            return databaseContext.Elements.Where(x => x.Tags.Where(x => x.Name == tag).Count() > 0).ToList();
        }

        public IEnumerable<string> GetAllTags()
        {
            List<string> tags = new List<string>();
            databaseContext.Elements.ToList().ForEach(x => x.Tags.ToList().ForEach(x => tags.Add(x.Name)));
            return tags.Distinct();
        }
    }
}
