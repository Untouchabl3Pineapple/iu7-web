using Allure.Xunit.Attributes;
using db_cp.Models;
using db_cp.Repository;
using db_cp.Interfaces;
using db_cp.Services;
using Moq;
using TestsCore.Builders;
using TestsCore.ObjectMothers;
using db_cp.Utils;

namespace UnitBL
{
    [AllureOwner("Untouchabl3pineapple")]
    [AllureSuite("EventsTypes Service Test")]

    [TestCaseOrderer(
        ordererTypeName: "TestsCore.Orderers.RandomOrderer",
        ordererAssemblyName: "TestsCore")]
    public class EventsTypesTests
    {
        private IMapper _mapper;

        public EventsTypesTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMappingProfile());
            });

            _mapper = mockMapper.CreateMapper();
        }

        [Fact]
        public void TestAdd()
        {
            var builder = new EventsTypesOM().CreateEventsTypes();
            var eventType = builder.buildBL();

            var mockEventsTypesRepository = new Mock<IEventsTypesRepository>();
            var eventsTypesService = new EventsTypesService(mockEventsTypesRepository.Object, _mapper);

            eventsTypesService.Add(eventType);

            mockEventsTypesRepository.Verify(repo => repo.Add(It.IsAny<EventsTypes>()), Times.Once);
        }

        [Fact]
        public void TestDelete()
        {
            var mockEventsTypesRepository = new Mock<IEventsTypesRepository>();
            var eventsTypesService = new EventsTypesService(mockEventsTypesRepository.Object, _mapper);

            eventsTypesService.Delete(1);

            mockEventsTypesRepository.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void TestUpdate()
        {
            var builder = new EventsTypesOM().CreateEventsTypes();
            var eventType = builder.build();
            var edit_eventtype = builder.withEventType("Detail curve").buildBL();

            var mockEventsTypesRepository = new Mock<IEventsTypesRepository>();
            mockEventsTypesRepository.Setup(repo => repo.GetByID(1)).Returns(
                eventType);
            var eventsTypesService = new EventsTypesService(mockEventsTypesRepository.Object, _mapper);

            eventsTypesService.Update(edit_eventtype);

            mockEventsTypesRepository.Verify(repo => repo.Update(It.IsAny<EventsTypes>()), Times.Once);
        }

        [Fact]
        public void TestGetAll()
        {
            var mockEventsTypesRepository = new Mock<IEventsTypesRepository>();
            var eventsTypesService = new EventsTypesService(mockEventsTypesRepository.Object, _mapper);

            eventsTypesService.GetAll();

            mockEventsTypesRepository.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public void TestGetById()
        {
            var mockEventsTypesRepository = new Mock<IEventsTypesRepository>();
            var eventsTypesService = new EventsTypesService(mockEventsTypesRepository.Object, _mapper);

            eventsTypesService.GetByID(1);

            mockEventsTypesRepository.Verify(repo => repo.GetByID(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void TestGetByEventType()
        {
            var mockEventsTypesRepository = new Mock<IEventsTypesRepository>();
            var eventsTypesService = new EventsTypesService(mockEventsTypesRepository.Object, _mapper);

            eventsTypesService.GetByEventType("type");

            mockEventsTypesRepository.Verify(repo => repo.GetByEventType(It.IsAny<string>()), Times.Once);
        }
    }
}