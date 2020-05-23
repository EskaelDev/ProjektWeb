using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektWeb.Data.Models.Database
{
    public class Element
    {
        [Key]
        public int ElementId { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
