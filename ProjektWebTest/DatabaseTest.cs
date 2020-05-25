using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ProjektWeb;
using ProjektWeb.Data.Models.Database;
using ProjektWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProjektWebTest
{
    public class DatabaseTest
    {

        [Fact(DisplayName = "Test1 successful")]
        public void Test1()
        {
            var options = PrepareDatabase();
            using (var context = new DatabaseContext(options))
            {
                var databaseService = new DatabaseService(context);
                databaseService.AddElement(new Element{ ElementId = 11, Name = "TestName" });
            }

            using (var context = new DatabaseContext(options))
            {
                var databaseService = new DatabaseService(context);
                var employee = databaseService.GetAllElements().First();
                Assert.Equal("TestName", employee.Name);

                var employee2 = databaseService.GetElementById(11);
                Assert.Equal("TestName", employee2.Name);

                var employee3 = databaseService.GetElementByName("TestName");
                Assert.Equal(11, employee3.ElementId);
            }
        }

        [Fact(DisplayName = "Test2 successful")]
        public void Test2()
        {
            var options = PrepareDatabase();
            using (var context = new DatabaseContext(options))
            {
                var databaseService = new DatabaseService(context);
                databaseService.AddElement(new Element { ElementId = 11, Name = "TestName"});
                databaseService.AddTagToElementById(11, "test1");
                databaseService.AddTagToElementById(11, "test2");
                databaseService.AddElement(new Element { ElementId = 12, Name = "TestName2"});
                databaseService.AddTagToElementById(12, "test3");
                databaseService.AddTagToElementById(12, "test4");
                databaseService.AddElement(new Element { ElementId = 13, Name = "TestName3" });
                databaseService.AddTagToElementById(13, "test1");
            }

            using (var context = new DatabaseContext(options))
            {
                var databaseService = new DatabaseService(context);
                var tags = databaseService.GetAllTags();

                Assert.Equal(2, databaseService.GetTagsByElementId(11).Count());
                Assert.Equal(2, databaseService.GetTagsByElementId(12).Count());
                Assert.Equal(4, tags.Count());
                Assert.Equal(2, databaseService.GetElementsContainingTag("test1").Count());
            }
        }

        private DbContextOptions<DatabaseContext> PrepareDatabase()
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
