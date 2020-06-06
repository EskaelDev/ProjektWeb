using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektWeb.Data.Models.Database
{
    public class Rate
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int RateId { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int Score { get; set; }
        public string Comment { get; set; }
        [Required]
        [DefaultValue(true)]
        public bool IsDeleted { get; set; }

        public int ElementId { get; set; }
    }
}
