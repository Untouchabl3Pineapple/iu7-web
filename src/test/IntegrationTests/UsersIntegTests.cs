using AutoMapper;
using Microsoft.EntityFrameworkCore;
using db_cp.Models;
using db_cp.Services;
using TestsCore.ObjectMothers;
using db_cp.ModelsBL;
using Microsoft.AspNetCore.Http.HttpResults;

namespace IntegrationTests
{
    [Collection(nameof(NonParallelCollection))]
    public class UsersTests
    {
        private readonly bool skip = Environment.GetEnvironmentVariable("skip") == "true";
        private readonly AppDBContext dbContext;
        public UsersTests()
        {
            dbContext = DbHelper.GetContext();
            DbHelper.ClearDb();
        }

        private bool AreUsersEqual(UsersBL model1, UsersBL model2)
        {
            return model1.Login == model2.Login
                && model1.Password == model2.Password
                && model1.Permission == model2.Permission
                && model1.Name == model2.Name
                && model1.Surname == model2.Surname
                && model1.MiddleName == model2.MiddleName;
        }

        [SkippableFact]
        public void TestAdd()
        {
            Skip.If(skip);

            var builder = new UsersOM().CreateUsers();
            var user = builder.buildBL();

            var usersService = DbHelper.GetRequiredService<UsersService>();

            var created = usersService.Add(user);

            Assert.True(AreUsersEqual(user, created));

            DbHelper.ClearDb();
        }

        [SkippableFact]
        public void TestDelete()
        {
            Skip.If(skip);

            var builder = new UsersOM().CreateUsers();
            var user = builder.build();

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var usersService = DbHelper.GetRequiredService<UsersService>();
            usersService.Delete("admin");

            Assert.Null(dbContext.Users.FirstOrDefault(m => m.Login == user.Login));

            DbHelper.ClearDb();
        }

        [SkippableFact]
        public void TestUpdate()
        {
            Skip.If(skip);

            var builder = new UsersOM().CreateUsers();
            var user = builder.build();
            var edit_user = builder.withName("gadzhi").buildBL();

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var usersService = DbHelper.GetRequiredService<UsersService>();
            var updated = usersService.Update(edit_user);

            Assert.True(AreUsersEqual(edit_user, updated));

            DbHelper.ClearDb();
        }

        [SkippableFact]
        public void TestGetAll()
        {
            Skip.If(skip);

            var builders = new UsersOM().CreateRange();
            var users = builders.Select(b => b.build()).ToList();
            var usersBL = builders.Select(b => b.buildBL()).ToList();

            dbContext.Users.Add(users[0]);
            dbContext.Users.Add(users[1]);
            dbContext.SaveChanges();

            var usersService = DbHelper.GetRequiredService<UsersService>();

            var getres = usersService.GetAll().OrderBy(m => m.Login).ToList();

            Assert.True(AreUsersEqual(usersBL[0], getres[0]));
            Assert.True(AreUsersEqual(usersBL[1], getres[1]));

            DbHelper.ClearDb();
        }

        [SkippableFact]
        public void TestGetByLogin()
        {
            Skip.If(skip);

            var builder = new UsersOM().CreateUsers();
            var user = builder.build();
            var userBL = builder.buildBL();

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var usersService = DbHelper.GetRequiredService<UsersService>();
            var getres = usersService.GetByLogin(user.Login);

            Assert.True(AreUsersEqual(userBL, getres));

            DbHelper.ClearDb();
        }
    }
}
