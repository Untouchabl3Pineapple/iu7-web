using AutoMapper;
using db_cp.Models;
using Microsoft.EntityFrameworkCore;

namespace UnitDA
{
    [AllureOwner("Untouchabl3pineapple")]
    [AllureSuite("Events Repository Test")]

    [TestCaseOrderer(
        ordererTypeName: "TestsCore.Orderers.RandomOrderer",
        ordererAssemblyName: "TestsCore")]
    public class EventsTests
    {
        [Fact]
        public void TestAdd()
        {
            var builder = new EventsOM().CreateEvents();
            var eevent = builder.build();

            using var dbContext = ContextCreator.CreateContext();
            var repository = new EventsRepository(dbContext);

            var created = repository.Add(eevent);

            Assert.Equal(eevent, created);
        }

        [Fact]
        public void TestDelete()
        {
            var builder = new EventsOM().CreateEvents();
            var eevent = builder.build();

            using var dbContext = ContextCreator.CreateContext();
            var repository = new EventsRepository(dbContext);

            repository.Add(eevent);

            repository.Delete(eevent.ID);

            Assert.Null(dbContext.Events.FirstOrDefault(m => m.ID == eevent.ID));
        }

        [Fact]
        public void TestUpdate()
        {
            var builder = new EventsOM().CreateEvents();
            var eevent = builder.build();
            var edit_event = builder.withDetectingTime(DateTime.UtcNow).build();

            using var dbContext = ContextCreator.CreateContext();
            var repository = new EventsRepository(dbContext);

            repository.Add(eevent);

            var updated = repository.Update(edit_event);

            Assert.Equal(edit_event.DetectingTime, updated.DetectingTime);
        }

        [Fact]
        public void TestGetAll()
        {
            var builders = new EventsOM().CreateRange();
            var events = builders.Select(b => b.build()).ToList();

            using var dbContext = ContextCreator.CreateContext();
            var repository = new EventsRepository(dbContext);

            repository.Add(events[0]);
            repository.Add(events[1]);

            var getres = repository.GetAll();

            Assert.Equal(events, getres);
        }

        [Fact]
        public void TestGetById()
        {
            var builder = new EventsOM().CreateEvents();
            var eevent = builder.build();

            using var dbContext = ContextCreator.CreateContext();
            var repository = new EventsRepository(dbContext);

            var created = repository.Add(eevent);
            var getres = repository.GetByID(eevent.ID);

            Assert.Equal(created, getres);
        }
    }
}