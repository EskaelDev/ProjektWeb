using Newtonsoft.Json;
using ProjektWeb.Data.Models.Database;
using System.Collections.Generic;

namespace ProjektWeb.Controllers
{
    public class ElementViewModel
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
        [JsonIgnore]
        public string Path { get; set; }
    }
}