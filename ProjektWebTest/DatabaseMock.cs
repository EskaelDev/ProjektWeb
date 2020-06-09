using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProjektWeb;
using ProjektWeb.Controllers;
using ProjektWeb.Data.Entities;
using ProjektWeb.Data.Models.Database;
using ProjektWeb.Helpers;
using ProjektWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace ProjektWebTest
{
    internal static class DatabaseMock
    {
        internal static DbContextOptions<DatabaseContext> PrepareDatabase()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<DatabaseContext>().UseSqlite(connection).Options;

            using (var context = new DatabaseContext(options))
            {
                context.Database.EnsureCreated();
            }

            return options;
        }
    }
}
