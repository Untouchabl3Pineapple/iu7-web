using Allure.Xunit.Attributes;
using db_cp.Models;
using db_cp.Repository;
using db_cp.Interfaces;
using db_cp.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsCore.ObjectMothers;
using db_cp.Utils;

namespace UnitBL
{
    [AllureOwner("Untouchabl3pineapple")]
    [AllureSuite("Events Service Test")]

    [TestCaseOrderer(
        ordererTypeName: "TestsCore.Orderers.RandomOrderer",
        ordererAssemblyName: "TestsCore")]
    public class EventsTests
    {
        private IMapper _mapper;

        public EventsTests()
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
            var builder = new EventsOM().CreateEvents();
            var eevent = builder.buildBL();

            var mockEventsRepository = new Mock<IEventsRepository>();
            var eventsService = new EventsService(mockEventsRepository.Object, _mapper);

            eventsService.Add(eevent);

            mockEventsRepository.Verify(repo => repo.Add(It.IsAny<Events>()), Times.Once);
        }

        [Fact]
        public void TestDelete()
        {
            var mockEventsRepository = new Mock<IEventsRepository>();
            var eventsService = new EventsService(mockEventsRepository.Object, _mapper);

            eventsService.Delete(1);

            mockEventsRepository.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void TestUpdate()
        {
            var builder = new EventsOM().CreateEvents();
            var eevent = builder.build();
            var edit_event = builder.withDetectingTime(DateTime.UtcNow).buildBL();

            var mockEventsRepository = new Mock<IEventsRepository>();
            mockEventsRepository.Setup(repo => repo.GetByID(1)).Returns(
                eevent);
            var eventsService = new EventsService(mockEventsRepository.Object, _mapper);

            eventsService.Update(edit_event);

            mockEventsRepository.Verify(repo => repo.Update(It.IsAny<Events>()), Times.Once);
        }

        [Fact]
        public void TestGetAll()
        {
            var mockEventsRepository = new Mock<IEventsRepository>();
            var eventsService = new EventsService(mockEventsRepository.Object, _mapper);

            eventsService.GetAll();

            mockEventsRepository.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public void TestGetById()
        {
            var mockEventsRepository = new Mock<IEventsRepository>();
            var eventsService = new EventsService(mockEventsRepository.Object, _mapper);

            eventsService.GetByID(1);

            mockEventsRepository.Verify(repo => repo.GetByID(It.IsAny<int>()), Times.Once);
        }
    }
}