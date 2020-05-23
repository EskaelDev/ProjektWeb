using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektWeb.Data.Models.Database
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        public string Name { get; set; }

        [ForeignKey("Element")]
        public int ElementId { get; set; }
        public Element Element { get; set; }
    }
}
