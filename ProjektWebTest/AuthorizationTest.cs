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
    public class AuthorizationTest
    {
        [Fact(DisplayName = "Register succesfull")]
        public async Task RegisterTest()
        {
            DbContextOptions<DatabaseContext> options = DatabaseMock.PrepareDatabase();

            User testUser = new User { Email = "test@test.test", Name = "test", Username = "test", Password = "TestPassPhrase" };

            using (var dbContext = new DatabaseContext(options))
            {
                IDatabaseService dbService = new DatabaseService(dbContext);
                var appSettings = new AppSettings { Secret = "762bdad7df203faddef523" };
                IOptions<AppSettings> option = Options.Create(appSettings);
                IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
                IUserService userService = new UserService(option, dbService, httpContextAccessor);


                User resultOfRegistration = await userService.Register(testUser);

                var userFromDb = await dbService.GetUserById(resultOfRegistration.Id).FirstOrDefaultAsync();
                Assert.False(userFromDb.IsDeleted);
                Assert.Equal(userFromDb.Id, resultOfRegistration.Id);
                Assert.Equal(userFromDb.Name, testUser.Name);
                Assert.Equal(userFromDb.NormalizedEmail, testUser.Email.ToUpper());
            }
        }

        [Fact(DisplayName = "Login succesfull")]
        public async Task LoginTest()
        {
            DbContextOptions<DatabaseContext> options = DatabaseMock.PrepareDatabase();

            User registerUser = new User { Email = "test@test.test", Name = "test", Username = "test", Password = "TestPassPhrase" };
            registerUser.NormalizedEmail = registerUser.Email.ToUpper();
            registerUser.Salt = Security.CreateSalt();
            registerUser.Password = Security.HashPassword(registerUser.Password, registerUser.Salt);

            using (var dbContext = new DatabaseContext(options))
            {
                var databaseService = new DatabaseService(dbContext);
                databaseService.AddUser(registerUser);
                
            }

            User loginUser = new User { Email = "test@test.test", Name = "test", Username = "test", Password = "TestPassPhrase" };
            loginUser.NormalizedEmail = loginUser.Email.ToUpper();
            using (var dbContext = new DatabaseContext(options))
            {
                IDatabaseService dbService = new DatabaseService(dbContext);
                var appSettings = new AppSettings { Secret = "762bdad7df203faddef523" };
                IOptions<AppSettings> option = Options.Create(appSettings);
                IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
                IUserService userService = new UserService(option, dbService, httpContextAccessor);

                User result = await userService.Authenticate(loginUser.NormalizedEmail, loginUser.Password);

                Assert.NotNull(result.Token);

            }
        }
    }
}
