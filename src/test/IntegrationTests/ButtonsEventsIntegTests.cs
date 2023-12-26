using AutoMapper;
using Microsoft.EntityFrameworkCore;
using db_cp.Models;
using db_cp.Interfaces;
using db_cp.Services;
using TestsCore.ObjectMothers;
using TestsCore.Builders;
using db_cp.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using db_cp.ModelsBL;

namespace IntegrationTests
{
    [Collection(nameof(NonParallelCollection))]
    public class ButtonsEventsTests
    {
        private readonly bool skip = Environment.GetEnvironmentVariable("skip") == "true";
        private readonly AppDBContext dbContext;

        public ButtonsEventsTests() 
        {
            dbContext = DbHelper.GetContext();
            DbHelper.ClearDb();
        }

        private bool AreButtonsEventsEqual(ButtonsEventsBL model1, ButtonsEventsBL model2)
        {
            return model1.ID == model2.ID
                && model1.ButtonColor == model2.ButtonColor
                && model1.Number == model2.Number;
        }

        [SkippableFact]
        public void TestAdd()
        {
            Skip.If(skip);

            var builder = new ButtonsEventsOM().CreateButtonsEvents();
            var buttonevent = builder.buildBL();

            var buttonsEventsService = DbHelper.GetRequiredService<ButtonsEventsService>();

            var created = buttonsEventsService.Add(buttonevent);

            Assert.True(AreButtonsEventsEqual(buttonevent, created));

            DbHelper.ClearDb();
        }

        [SkippableFact]
        public void TestDelete()
        {
            Skip.If(skip);

            var builder = new ButtonsEventsOM().CreateButtonsEvents();
            var buttonevent = builder.build();

            dbContext.ButtonsEvents.Add(buttonevent);
            dbContext.SaveChanges();

            var buttonsEventsService = DbHelper.GetRequiredService<ButtonsEventsService>();
            buttonsEventsService.Delete(1);

            Assert.Null(dbContext.ButtonsEvents.FirstOrDefault(m => m.ID == buttonevent.ID));

            DbHelper.ClearDb();
        }

        [SkippableFact]
        public void TestUpdate()
        {
            Skip.If(skip);

            var builder = new ButtonsEventsOM().CreateButtonsEvents();
            var buttonevent = builder.build();
            var edit_buttonevent = builder.withButtonColor("GREEN").buildBL();

            dbContext.ButtonsEvents.Add(buttonevent);
            dbContext.SaveChanges();

            var buttonsEventsService = DbHelper.GetRequiredService<ButtonsEventsService>();
            var updated = buttonsEventsService.Update(edit_buttonevent);

            Assert.True(AreButtonsEventsEqual(edit_buttonevent, updated));

            DbHelper.ClearDb();
        }

        [SkippableFact]
        public void TestGetAll()
        {
            Skip.If(skip);

            var builders = new ButtonsEventsOM().CreateRange();
            var buttonsEvents = builders.Select(b => b.build()).ToList();
            var buttonsEventsBL = builders.Select(b => b.buildBL()).ToList();

            dbContext.ButtonsEvents.Add(buttonsEvents[0]);
            dbContext.ButtonsEvents.Add(buttonsEvents[1]);
            dbContext.SaveChanges();

            var buttonsEventsService = DbHelper.GetRequiredService<ButtonsEventsService>();
            var getres = buttonsEventsService.GetAll().OrderBy(m => m.ID).ToList();

            Assert.True(AreButtonsEventsEqual(buttonsEventsBL[0], getres[0]));
            Assert.True(AreButtonsEventsEqual(buttonsEventsBL[1], getres[1]));

            DbHelper.ClearDb();
        }

        [SkippableFact]
        public void TestGetById()
        {
            Skip.If(skip);

            var builder = new ButtonsEventsOM().CreateButtonsEvents();
            var buttonevent = builder.build();
            var buttoneventBL = builder.buildBL();

            dbContext.ButtonsEvents.Add(buttonevent);
            dbContext.SaveChanges();

            var buttonsEventsService = DbHelper.GetRequiredService<ButtonsEventsService>();
            var getres = buttonsEventsService.GetByID(buttonevent.ID);

            Assert.True(AreButtonsEventsEqual(buttoneventBL, getres));


            DbHelper.ClearDb();
        }
    }
}
