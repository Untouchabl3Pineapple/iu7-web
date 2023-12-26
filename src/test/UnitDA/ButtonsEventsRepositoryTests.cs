using AutoMapper;
using db_cp.Models;
using Microsoft.EntityFrameworkCore;

namespace UnitDA
{
    [AllureOwner("Untouchabl3pineapple")]
    [AllureSuite("ButtonsEvents Repository Test")]

    [TestCaseOrderer(
        ordererTypeName: "TestsCore.Orderers.RandomOrderer",
        ordererAssemblyName: "TestsCore")]
    public class ButtonsEventsTests
    {
        [Fact]
        public void TestAdd()
        {
            var builder = new ButtonsEventsOM().CreateButtonsEvents();
            var buttonevent = builder.build();

            using var dbContext = ContextCreator.CreateContext();
            var repository = new ButtonsEventsRepository(dbContext);

            var created = repository.Add(buttonevent);

            Assert.Equal(buttonevent, created);
        }

        [Fact]
        public void TestDelete()
        {
            var builder = new ButtonsEventsOM().CreateButtonsEvents();
            var buttonevent = builder.build();

            using var dbContext = ContextCreator.CreateContext();
            var repository = new ButtonsEventsRepository(dbContext);

            repository.Add(buttonevent);

            repository.Delete(buttonevent.ID);

            Assert.Null(dbContext.ButtonsEvents.FirstOrDefault(m => m.ID == buttonevent.ID));
        }

        [Fact]
        public void TestUpdate()
        {
            var builder = new ButtonsEventsOM().CreateButtonsEvents();
            var buttonevent = builder.build();
            var edit_buttonevent = builder.withButtonColor("GREEN").build();

            using var dbContext = ContextCreator.CreateContext();
            var repository = new ButtonsEventsRepository(dbContext);

            repository.Add(buttonevent);

            var updated = repository.Update(edit_buttonevent);

            Assert.Equal(edit_buttonevent.ButtonColor, updated.ButtonColor);
        }

        [Fact]
        public void TestGetAll()
        {
            var builders = new ButtonsEventsOM().CreateRange();
            var buttonsEvents = builders.Select(b => b.build()).ToList();

            using var dbContext = ContextCreator.CreateContext();
            var repository = new ButtonsEventsRepository(dbContext);

            repository.Add(buttonsEvents[0]);
            repository.Add(buttonsEvents[1]);

            var getres = repository.GetAll();

            Assert.Equal(buttonsEvents, getres);
        }

        [Fact]
        public void TestGetById()
        {
            var builder = new ButtonsEventsOM().CreateButtonsEvents();
            var buttonevent = builder.build();

            using var dbContext = ContextCreator.CreateContext();
            var repository = new ButtonsEventsRepository(dbContext);

            var created = repository.Add(buttonevent);
            var getres = repository.GetByID(buttonevent.ID);

            Assert.Equal(created, getres);
        }
    }
}