using AutoMapper;
using db_cp.Models;
using db_cp.ModelsBL;
using Microsoft.EntityFrameworkCore;

namespace UnitDA
{
    [AllureOwner("Untouchabl3pineapple")]
    [AllureSuite("ButtonsPosts Repository Test")]

    [TestCaseOrderer(
        ordererTypeName: "TestsCore.Orderers.RandomOrderer",
        ordererAssemblyName: "TestsCore")]
    public class ButtonsPostsTests
    {
        [Fact]
        public void TestAdd()
        {
            var builder = new ButtonsPostsOM().CreateButtonsPosts();
            var buttonpost = builder.build();

            using var dbContext = ContextCreator.CreateContext();
            var repository = new ButtonsPostsRepository(dbContext);

            var created = repository.Add(buttonpost);

            Assert.Equal(buttonpost, created);
        }

        [Fact]
        public void TestDelete()
        {
            var builder = new ButtonsPostsOM().CreateButtonsPosts();
            var buttonpost = builder.build();

            using var dbContext = ContextCreator.CreateContext();
            var repository = new ButtonsPostsRepository(dbContext);

            repository.Add(buttonpost);

            repository.Delete(buttonpost.Post);

            Assert.Null(dbContext.ButtonsEvents.FirstOrDefault(m => m.ID == buttonpost.Post));
        }

        [Fact]
        public void TestUpdate()
        {
            var builder = new ButtonsPostsOM().CreateButtonsPosts();
            var buttonpost = builder.build();
            var edit_buttonpost = builder.withLeftColor("GREEN").build();

            using var dbContext = ContextCreator.CreateContext();
            var repository = new ButtonsPostsRepository(dbContext);

            repository.Add(buttonpost);

            var updated = repository.Update(edit_buttonpost);

            Assert.Equal(edit_buttonpost.LeftColor, updated.LeftColor);
        }

        [Fact]
        public void TestGetAll()
        {
            var builders = new ButtonsPostsOM().CreateRange();
            var buttonsPosts = builders.Select(b => b.build()).ToList();

            using var dbContext = ContextCreator.CreateContext();
            var repository = new ButtonsPostsRepository(dbContext);

            repository.Add(buttonsPosts[0]);
            repository.Add(buttonsPosts[1]);

            var getres = repository.GetAll();

            Assert.Equal(buttonsPosts, getres);
        }

        [Fact]
        public void TestGetByPost()
        {
            var builder = new ButtonsPostsOM().CreateButtonsPosts();
            var buttonpost = builder.build();

            using var dbContext = ContextCreator.CreateContext();
            var repository = new ButtonsPostsRepository(dbContext);

            var created = repository.Add(buttonpost);
            var getres = repository.GetByPost(buttonpost.Post);

            Assert.Equal(created, getres);
        }
    }
}