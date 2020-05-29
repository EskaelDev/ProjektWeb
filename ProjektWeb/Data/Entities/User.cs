using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

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
    }

    public enum UserRoles
    {
        admin,
        user
    }
}
