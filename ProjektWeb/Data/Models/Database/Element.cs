using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektWeb.Data.Models.Database
{
    public class Element
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        public string ImagePath { get; set; }

        [Required]
        [DefaultValue(true)]
        public bool IsDeleted { get; set; }

        [ForeignKey("ElementId")]
        public virtual List<Tag> Tags { get; set; }
    }
}
