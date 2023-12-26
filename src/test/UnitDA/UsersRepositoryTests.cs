using AutoMapper;
using db_cp.Models;
using Microsoft.EntityFrameworkCore;

namespace UnitDA
{
    [AllureOwner("Untouchabl3pineapple")]
    [AllureSuite("Users Repository Test")]

    [TestCaseOrderer(
        ordererTypeName: "TestsCore.Orderers.RandomOrderer",
        ordererAssemblyName: "TestsCore")]
    public class UsersTests
    {
        [Fact]
        public void TestAdd()
        {
            var builder = new UsersOM().CreateUsers();
            var user = builder.build();

            using var dbContext = ContextCreator.CreateContext();
            var repository = new UsersRepository(dbContext);

            var created = repository.Add(user);

            Assert.Equal(user, created);
        }

        [Fact]
        public void TestDelete()
        {
            var builder = new UsersOM().CreateUsers();
            var user = builder.build();

            using var dbContext = ContextCreator.CreateContext();
            var repository = new UsersRepository(dbContext);

            repository.Add(user);

            repository.Delete(user.Login);

            Assert.Null(dbContext.Users.FirstOrDefault(m => m.Login == user.Login));
        }

        [Fact]
        public void TestUpdate()
        {
            var builder = new UsersOM().CreateUsers();
            var user = builder.build();
            var edit_user = builder.withName("gadzhi").build();

            using var dbContext = ContextCreator.CreateContext();
            var repository = new UsersRepository(dbContext);

            repository.Add(user);

            var updated = repository.Update(edit_user);

            Assert.Equal(edit_user.Name, updated.Name);
        }

        [Fact]
        public void TestGetAll()
        {
            var builders = new UsersOM().CreateRange();
            var users = builders.Select(b => b.build()).ToList();

            using var dbContext = ContextCreator.CreateContext();
            var repository = new UsersRepository(dbContext);

            repository.Add(users[0]);
            repository.Add(users[1]);

            var getres = repository.GetAll();

            Assert.Equal(users, getres);
        }

        [Fact]
        public void TestGetByLogin()
        {
            var builder = new UsersOM().CreateUsers();
            var user = builder.build();

            using var dbContext = ContextCreator.CreateContext();
            var repository = new UsersRepository(dbContext);

            var created = repository.Add(user);
            var getres = repository.GetByLogin(user.Login);

            Assert.Equal(created, getres);
        }
    }
}