using AutoMapper;
using db_cp.Models;
using Microsoft.EntityFrameworkCore;

namespace UnitDA
{
    [AllureOwner("Untouchabl3pineapple")]
    [AllureSuite("EventsTypes Repository Test")]

    [TestCaseOrderer(
        ordererTypeName: "TestsCore.Orderers.RandomOrderer",
        ordererAssemblyName: "TestsCore")]
    public class EventsTypesTests
    {
        [Fact]
        public void TestAdd()
        {
            var builder = new EventsTypesOM().CreateEventsTypes();
            var eventType = builder.build();

            using var dbContext = ContextCreator.CreateContext();
            var repository = new EventsTypesRepository(dbContext);

            var created = repository.Add(eventType);

            Assert.Equal(eventType, created);
        }

        [Fact]
        public void TestDelete()
        {
            var builder = new EventsTypesOM().CreateEventsTypes();
            var eventType = builder.build();

            using var dbContext = ContextCreator.CreateContext();
            var repository = new EventsTypesRepository(dbContext);

            repository.Add(eventType);

            repository.Delete(eventType.ID);

            Assert.Null(dbContext.EventsTypes.FirstOrDefault(m => m.ID == eventType.ID));
        }

        [Fact]
        public void TestUpdate()
        {
            var builder = new EventsTypesOM().CreateEventsTypes();
            var eventType = builder.build();
            var edit_eventtype = builder.withEventType("Detail curve").build();

            using var dbContext = ContextCreator.CreateContext();
            var repository = new EventsTypesRepository(dbContext);

            repository.Add(eventType);

            var updated = repository.Update(edit_eventtype);

            Assert.Equal(edit_eventtype.EventType, updated.EventType);
        }

        [Fact]
        public void TestGetAll()
        {
            var builders = new EventsTypesOM().CreateRange();
            var eventsTypes = builders.Select(b => b.build()).ToList();

            using var dbContext = ContextCreator.CreateContext();
            var repository = new EventsTypesRepository(dbContext);

            repository.Add(eventsTypes[0]);
            repository.Add(eventsTypes[1]);

            var getres = repository.GetAll();

            Assert.Equal(eventsTypes, getres);
        }

        [Fact]
        public void TestGetById()
        {
            var builder = new EventsTypesOM().CreateEventsTypes();
            var eventType = builder.build();

            using var dbContext = ContextCreator.CreateContext();
            var repository = new EventsTypesRepository(dbContext);

            var created = repository.Add(eventType);
            var getres = repository.GetByID(eventType.ID);

            Assert.Equal(created, getres);
        }

        [Fact]
        public void TestGetByEventType()
        {
            var builder = new EventsTypesOM().CreateEventsTypes();
            var buttonevent = builder.build();

            using var dbContext = ContextCreator.CreateContext();
            var repository = new EventsTypesRepository(dbContext);

            var created = repository.Add(buttonevent);
            var getres = repository.GetByEventType(buttonevent.EventType);

            Assert.Equal(created, getres);
        }
    }
}