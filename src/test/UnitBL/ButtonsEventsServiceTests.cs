using Allure.Xunit.Attributes;
using Moq;
using db_cp.Models;
using db_cp.Repository;
using db_cp.Interfaces;
using db_cp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsCore.ObjectMothers;
using AutoMapper;
using db_cp.Utils;


namespace UnitBL
{
    [AllureOwner("Untouchabl3pineapple")]
    [AllureSuite("ButtonsEvents Service Test")]

    [TestCaseOrderer(
        ordererTypeName: "TestsCore.Orderers.RandomOrderer",
        ordererAssemblyName: "TestsCore")]
    public class ButtonsEventsTests
    {
        private IMapper _mapper;

        public ButtonsEventsTests()
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
            var builder = new ButtonsEventsOM().CreateButtonsEvents();
            var buttonevent = builder.buildBL();

            var mockButtonsEventsRepository = new Mock<IButtonsEventsRepository>();
            var buttonsEventsService = new ButtonsEventsService(mockButtonsEventsRepository.Object, _mapper);

            buttonsEventsService.Add(buttonevent);

            mockButtonsEventsRepository.Verify(repo => repo.Add(It.IsAny<ButtonsEvents>()), Times.Once);
        }

        [Fact]
        public void TestDelete()
        {
            var mockButtonsEventsRepository = new Mock<IButtonsEventsRepository>();
            var buttonsEventsService = new ButtonsEventsService(mockButtonsEventsRepository.Object, _mapper);

            buttonsEventsService.Delete(1);

            mockButtonsEventsRepository.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void TestUpdate()
        {
            var builder = new ButtonsEventsOM().CreateButtonsEvents();
            var buttonevent = builder.build();
            var edit_buttonevent = builder.withButtonColor("GREEN").buildBL();

            var mockButtonsEventsRepository = new Mock<IButtonsEventsRepository>();
            mockButtonsEventsRepository.Setup(repo => repo.GetByID(1)).Returns(
                buttonevent);
            var buttonsEventsService = new ButtonsEventsService(mockButtonsEventsRepository.Object, _mapper);

            buttonsEventsService.Update(edit_buttonevent);

            mockButtonsEventsRepository.Verify(repo => repo.Update(It.IsAny<ButtonsEvents>()), Times.Once);
        }

        [Fact]
        public void TestGetAll()
        {
            var mockButtonsEventsRepository = new Mock<IButtonsEventsRepository>();
            var buttonsEventsService = new ButtonsEventsService(mockButtonsEventsRepository.Object, _mapper);
            
            buttonsEventsService.GetAll();

            mockButtonsEventsRepository.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public void TestGetById()
        {
            var mockButtonsEventsRepository = new Mock<IButtonsEventsRepository>();
            var buttonsEventsService = new ButtonsEventsService(mockButtonsEventsRepository.Object, _mapper);

            buttonsEventsService.GetByID(1);

            mockButtonsEventsRepository.Verify(repo => repo.GetByID(It.IsAny<int>()), Times.Once);
        }
    }
}