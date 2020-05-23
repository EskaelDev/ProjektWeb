using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektWeb.Data.Models.Database
{
    public class Rate
    {
        [Key]
        public int RateId { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int Score { get; set; }
        public string Comment { get; set; }
    }
}
