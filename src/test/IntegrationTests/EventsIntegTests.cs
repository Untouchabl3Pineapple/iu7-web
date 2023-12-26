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
    public class EventsTests
    {
        private readonly bool skip = Environment.GetEnvironmentVariable("skip") == "true";
        private readonly AppDBContext dbContext;
        public EventsTests()
        {
            dbContext = DbHelper.GetContext();
            DbHelper.ClearDb();
        }

        private bool AreEventsEqual(EventsBL model1, EventsBL model2)
        {
            return model1.ID == model2.ID
                && model1.ButtonEvent_ID == model2.ButtonEvent_ID
                && model1.DetectingTime == model2.DetectingTime;
        }

        [SkippableFact]
        public void TestAdd()
        {
            Skip.If(skip);

            var builder = new EventsOM().CreateEvents();
            var eevent = builder.buildBL();

            var eventsService = DbHelper.GetRequiredService<EventsService>();

            var created = eventsService.Add(eevent);

            Assert.True(AreEventsEqual(eevent, created));

            DbHelper.ClearDb();
        }

        [SkippableFact]
        public void TestDelete()
        {
            Skip.If(skip);

            var builder = new EventsOM().CreateEvents();
            var eevent = builder.build();

            dbContext.Events.Add(eevent);
            dbContext.SaveChanges();

            var buttonsEventsService = DbHelper.GetRequiredService<EventsService>();
            buttonsEventsService.Delete(1);

            Assert.Null(dbContext.Events.FirstOrDefault(m => m.ID == eevent.ID));

            DbHelper.ClearDb();
        }

        [SkippableFact]
        public void TestUpdate()
        {
            Skip.If(skip);

            var builder = new EventsOM().CreateEvents();
            var eevent = builder.build();
            var edit_event = builder.withDetectingTime(DateTime.UtcNow).buildBL();

            dbContext.Events.Add(eevent);
            dbContext.SaveChanges();

            var eventsService = DbHelper.GetRequiredService<EventsService>();
            var updated = eventsService.Update(edit_event);

            Assert.True(AreEventsEqual(edit_event, updated));

            DbHelper.ClearDb();
        }

        [SkippableFact]
        public void TestGetAll()
        {
            Skip.If(skip);

            var builders = new EventsOM().CreateRange();
            var events = builders.Select(b => b.build()).ToList();
            var eventsBL = builders.Select(b => b.buildBL()).ToList();

            dbContext.Events.Add(events[0]);
            dbContext.Events.Add(events[1]);
            dbContext.SaveChanges();

            var eventsService = DbHelper.GetRequiredService<EventsService>();

            var getres = eventsService.GetAll().OrderBy(m => m.ID).ToList();

            Assert.True(AreEventsEqual(eventsBL[0], getres[0]));
            Assert.True(AreEventsEqual(eventsBL[1], getres[1]));

            DbHelper.ClearDb();
        }

        [SkippableFact]
        public void TestGetById()
        {
            Skip.If(skip);

            var builder = new EventsOM().CreateEvents();
            var eevent = builder.build();
            var eventBL = builder.buildBL();

            dbContext.Events.Add(eevent);
            dbContext.SaveChanges();

            var eventsService = DbHelper.GetRequiredService<EventsService>();
            var getres = eventsService.GetByID(eevent.ID);

            Assert.True(AreEventsEqual(eventBL, getres));

            DbHelper.ClearDb();
        }

    }
}
