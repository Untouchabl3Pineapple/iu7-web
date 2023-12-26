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
    public class ButtonsPostsTests
    {
        private readonly bool skip = Environment.GetEnvironmentVariable("skip") == "true";
        private readonly AppDBContext dbContext;
        public ButtonsPostsTests()
        {
            dbContext = DbHelper.GetContext();
            DbHelper.ClearDb();
        }

        private bool AreButtonsPostsEqual(ButtonsPostsBL model1, ButtonsPostsBL model2)
        {
            return model1.Post == model2.Post
                && model1.LeftSide == model2.LeftSide
                && model1.RightSide == model2.RightSide
                && model1.LeftColor == model2.LeftColor
                && model1.RightColor == model2.RightColor;
        }

        [SkippableFact]
        public void TestAdd()
        {
            Skip.If(skip);

            var builder = new ButtonsPostsOM().CreateButtonsPosts();
            var buttonpost = builder.buildBL();

            var buttonsPostsService = DbHelper.GetRequiredService<ButtonsPostsService>();

            var created = buttonsPostsService.Add(buttonpost);

            Assert.True(AreButtonsPostsEqual(buttonpost, created));

            DbHelper.ClearDb();
        }

        [SkippableFact]
        public void TestDelete()
        {
            Skip.If(skip);

            var builder = new ButtonsPostsOM().CreateButtonsPosts();
            var buttonpost = builder.build();

            dbContext.ButtonsPosts.Add(buttonpost);
            dbContext.SaveChanges();

            var buttonsPostsService = DbHelper.GetRequiredService<ButtonsPostsService>();
            buttonsPostsService.Delete(1);

            Assert.Null(dbContext.ButtonsPosts.FirstOrDefault(m => m.Post == buttonpost.Post));

            DbHelper.ClearDb();
        }

        [SkippableFact]
        public void TestUpdate()
        {
            Skip.If(skip);

            var builder = new ButtonsPostsOM().CreateButtonsPosts();
            var buttonpost = builder.build();
            var edit_buttonpost = builder.withLeftColor("GREEN").buildBL();

            dbContext.ButtonsPosts.Add(buttonpost);
            dbContext.SaveChanges();

            var buttonsPostsService = DbHelper.GetRequiredService<ButtonsPostsService>();
            var updated = buttonsPostsService.Update(edit_buttonpost);

            Assert.True(AreButtonsPostsEqual(edit_buttonpost, updated));

            DbHelper.ClearDb();
        }

        [SkippableFact]
        public void TestGetAll()
        {
            Skip.If(skip);

            var builders = new ButtonsPostsOM().CreateRange();
            var buttonsPosts = builders.Select(b => b.build()).ToList();
            var buttonsPostsBL = builders.Select(b => b.buildBL()).ToList();

            dbContext.ButtonsPosts.Add(buttonsPosts[0]);
            dbContext.ButtonsPosts.Add(buttonsPosts[1]);
            dbContext.SaveChanges();

            var buttonsPostsService = DbHelper.GetRequiredService<ButtonsPostsService>();
            var getres = buttonsPostsService.GetAll().OrderBy(m => m.Post).ToList();

            Assert.True(AreButtonsPostsEqual(buttonsPostsBL[0], getres[0]));
            Assert.True(AreButtonsPostsEqual(buttonsPostsBL[1], getres[1]));

            DbHelper.ClearDb();
        }

        [SkippableFact]
        public void TestGetByPost()
        {
            Skip.If(skip);

            var builder = new ButtonsPostsOM().CreateButtonsPosts();
            var buttonpost = builder.build();
            var buttonpostBL = builder.buildBL();

            dbContext.ButtonsPosts.Add(buttonpost);
            dbContext.SaveChanges();

            var buttonsPostsService = DbHelper.GetRequiredService<ButtonsPostsService>();
            var getres = buttonsPostsService.GetByPost(buttonpost.Post);

            Assert.True(AreButtonsPostsEqual(buttonpostBL, getres));

            DbHelper.ClearDb();
        }
    }
}
