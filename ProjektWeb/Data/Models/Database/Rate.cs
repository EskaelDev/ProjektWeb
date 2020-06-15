using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjektWeb.Data.Models.Database
{
    public class Rate
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int RateId { get; set; }
        [Required]
        public int Author { get; set; }
        public string AuthorName { get; set; }
        [Required]
        public int Score { get; set; }
        public string Comment { get; set; }
        [Required]
        [DefaultValue(false)]
        [JsonIgnore]
        public bool IsDeleted { get; set; }
        [JsonIgnore]
        public int ElementId { get; set; }
    }
}
