using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ProjektWeb;
using ProjektWeb.Data.Models.Database;
using ProjektWeb.Services;
using System;
using System.Linq;
using Xunit;

namespace ProjektWebTest
{
    public class DatabaseTest
    {

        [Fact(DisplayName = "Successful response Test1")]
        public void Test1()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<DatabaseContext>().UseSqlite(connection).Options;

            using (var context = new DatabaseContext(options))
            {
                context.Database.EnsureCreated();
            }

            using (var context = new DatabaseContext(options))
            {
                context.Elements.Add(new Element{ ElementId = 1, Name = "TestName" });
                context.SaveChanges();
            }

            using (var context = new DatabaseContext(options))
            {
                var databaseService = new DatabaseService(context);
                var employee = databaseService.GetAllElements().First();

                Assert.Equal("TestName", employee.Name);
            }
        }
    }

}
