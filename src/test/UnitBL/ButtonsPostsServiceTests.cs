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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestsCore.ObjectMothers;
using db_cp.Utils;

namespace UnitBL
{
    [AllureOwner("Untouchabl3pineapple")]
    [AllureSuite("ButtonsPosts Service Test")]

    [TestCaseOrderer(
        ordererTypeName: "TestsCore.Orderers.RandomOrderer",
        ordererAssemblyName: "TestsCore")]
    public class ButtonsPostsTests
    {
        private IMapper _mapper;

        public ButtonsPostsTests()
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
            var builder = new ButtonsPostsOM().CreateButtonsPosts();
            var buttonpost = builder.buildBL();

            var mockButtonsPostsRepository = new Mock<IButtonsPostsRepository>();
            var buttonsPostsService = new ButtonsPostsService(mockButtonsPostsRepository.Object, _mapper);

            buttonsPostsService.Add(buttonpost);

            mockButtonsPostsRepository.Verify(repo => repo.Add(It.IsAny<ButtonsPosts>()), Times.Once);
        }

        [Fact]
        public void TestDelete()
        {
            var mockButtonsPostsRepository = new Mock<IButtonsPostsRepository>();
            var buttonsPostsService = new ButtonsPostsService(mockButtonsPostsRepository.Object, _mapper);

            buttonsPostsService.Delete(1);

            mockButtonsPostsRepository.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void TestUpdate()
        {
            var builder = new ButtonsPostsOM().CreateButtonsPosts();
            var buttonpost = builder.build();
            var edit_buttonpost = builder.withLeftColor("GREEN").buildBL();

            var mockButtonsPostsRepository = new Mock<IButtonsPostsRepository>();
            mockButtonsPostsRepository.Setup(repo => repo.GetByPost(1)).Returns(
                buttonpost);
            var buttonsPostsService = new ButtonsPostsService(mockButtonsPostsRepository.Object, _mapper);

            buttonsPostsService.Update(edit_buttonpost);

            mockButtonsPostsRepository.Verify(repo => repo.Update(It.IsAny<ButtonsPosts>()), Times.Once);
        }

        [Fact]
        public void TestGetAll()
        {
            var mockButtonsPostsRepository = new Mock<IButtonsPostsRepository>();
            var buttonsPostsService = new ButtonsPostsService(mockButtonsPostsRepository.Object, _mapper);

            buttonsPostsService.GetAll();

            mockButtonsPostsRepository.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public void TestGetByPost()
        {
            var mockButtonsPostsRepository = new Mock<IButtonsPostsRepository>();
            var buttonsPostsService = new ButtonsPostsService(mockButtonsPostsRepository.Object, _mapper);

            buttonsPostsService.GetByPost(1);

            mockButtonsPostsRepository.Verify(repo => repo.GetByPost(It.IsAny<int>()), Times.Once);
        }
    }
}