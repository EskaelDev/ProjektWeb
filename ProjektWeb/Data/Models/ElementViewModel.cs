using System.Collections;
using System.Collections.Generic;

namespace ProjektWeb.Controllers
{
    public class ElementViewModel
    {
        public string Title{ get; set; }
        public List<string> Tags { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }
}