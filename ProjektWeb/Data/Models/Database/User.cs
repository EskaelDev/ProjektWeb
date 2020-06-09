using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProjektWeb.Data.Entities
{

    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Username { get; set; }
        public UserRoles Role { get; set; }

        public string Email { get; set; }

        public string NormalizedEmail { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public string Token { get; set; }
        [Required]
        [DefaultValue(true)]
        public bool IsDeleted { get; set; }

        [JsonIgnore]
        public byte[] Salt { get; set; }
    }

    public enum UserRoles
    {
        admin,
        user
    }
}
