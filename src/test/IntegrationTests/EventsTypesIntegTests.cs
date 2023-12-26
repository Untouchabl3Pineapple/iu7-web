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
    public class EventsTypesTests
    {
        private readonly bool skip = Environment.GetEnvironmentVariable("skip") == "true";
        private readonly AppDBContext dbContext;
        public EventsTypesTests()
        {
            dbContext = DbHelper.GetContext();
            DbHelper.ClearDb();
        }

        private bool AreEventsTypesEqual(EventsTypesBL model1, EventsTypesBL model2)
        {
            return model1.ID == model2.ID
                && model1.EventType == model2.EventType;
        }

        [SkippableFact]
        public void TestAdd()
        {
            Skip.If(skip);

            var builder = new EventsTypesOM().CreateEventsTypes();
            var eventtype = builder.buildBL();

            var eventsTypesService = DbHelper.GetRequiredService<EventsTypesService>();

            var created = eventsTypesService.Add(eventtype);

            Assert.True(AreEventsTypesEqual(eventtype, created));

            DbHelper.ClearDb();
        }

        [SkippableFact]
        public void TestDelete()
        {
            Skip.If(skip);

            var builder = new EventsTypesOM().CreateEventsTypes();
            var eventtype = builder.build();

            dbContext.EventsTypes.Add(eventtype);
            dbContext.SaveChanges();

            var eventsTypesService = DbHelper.GetRequiredService<EventsTypesService>();
            eventsTypesService.Delete(1);

            Assert.Null(dbContext.ButtonsEvents.FirstOrDefault(m => m.ID == eventtype.ID));

            DbHelper.ClearDb();
        }

        [SkippableFact]
        public void TestUpdate()
        {
            Skip.If(skip);

            var builder = new EventsTypesOM().CreateEventsTypes();
            var eventtype = builder.build();
            var edit_eventtype = builder.withEventType("Detail curve").buildBL();

            dbContext.EventsTypes.Add(eventtype);
            dbContext.SaveChanges();

            var eventsTypesService = DbHelper.GetRequiredService<EventsTypesService>();
            var updated = eventsTypesService.Update(edit_eventtype);

            Assert.True(AreEventsTypesEqual(edit_eventtype, updated));

            DbHelper.ClearDb();
        }

        [SkippableFact]
        public void TestGetAll()
        {
            Skip.If(skip);

            var builders = new EventsTypesOM().CreateRange();
            var eventsTypes = builders.Select(b => b.build()).ToList();
            var eventsTypesBL = builders.Select(b => b.buildBL()).ToList();

            dbContext.EventsTypes.Add(eventsTypes[0]);
            dbContext.EventsTypes.Add(eventsTypes[1]);
            dbContext.SaveChanges();

            var eventsTypesService = DbHelper.GetRequiredService<EventsTypesService>();
            var getres = eventsTypesService.GetAll().OrderBy(m => m.ID).ToList();

            Assert.True(AreEventsTypesEqual(eventsTypesBL[0], getres[0]));
            Assert.True(AreEventsTypesEqual(eventsTypesBL[1], getres[1]));

            DbHelper.ClearDb();
        }

        [SkippableFact]
        public void TestGetById()
        {
            Skip.If(skip);

            var builder = new EventsTypesOM().CreateEventsTypes();
            var eventtype = builder.build();
            var eventtypeBL = builder.buildBL();

            dbContext.EventsTypes.Add(eventtype);
            dbContext.SaveChanges();

            var eventsTypesService = DbHelper.GetRequiredService<EventsTypesService>();
            var getres = eventsTypesService.GetByID(eventtype.ID);

            Assert.True(AreEventsTypesEqual(eventtypeBL, getres));

            DbHelper.ClearDb();
        }

        [SkippableFact]
        public void TestGetByEventType()
        {
            Skip.If(skip);

            var builder = new EventsTypesOM().CreateEventsTypes();
            var eventtype = builder.build();
            var eventtypeBL = builder.buildBL();

            dbContext.EventsTypes.Add(eventtype);
            dbContext.SaveChanges();

            var eventsTypesService = DbHelper.GetRequiredService<EventsTypesService>();
            var getres = eventsTypesService.GetByEventType(eventtype.EventType);

            Assert.True(AreEventsTypesEqual(eventtypeBL, getres));

            DbHelper.ClearDb();
        }
    }
}
