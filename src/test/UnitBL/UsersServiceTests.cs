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
using TestsCore.Builders;
using TestsCore.ObjectMothers;
using db_cp.Utils;

namespace UnitBL
{
    [AllureOwner("Untouchabl3pineapple")]
    [AllureSuite("Users Service Test")]

    [TestCaseOrderer(
        ordererTypeName: "TestsCore.Orderers.RandomOrderer",
        ordererAssemblyName: "TestsCore")]
    public class UsersTests
    {
        private IMapper _mapper;

        public UsersTests()
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
            var builder = new UsersOM().CreateUsers();
            var user = builder.buildBL();

            var mockUsersRepository = new Mock<IUsersRepository>();
            var usersService = new UsersService(mockUsersRepository.Object, _mapper);

            usersService.Add(user);

            mockUsersRepository.Verify(repo => repo.Add(It.IsAny<Users>()), Times.Once);
        }

        [Fact]
        public void TestDelete()
        {
            var mockUsersRepository = new Mock<IUsersRepository>();
            var usersService = new UsersService(mockUsersRepository.Object, _mapper);

            usersService.Delete("admin");

            mockUsersRepository.Verify(repo => repo.Delete(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void TestUpdate()
        {
            var builder = new UsersOM().CreateUsers();
            var user = builder.build();
            var edit_user = builder.withName("gadzhi").buildBL();

            var mockUsersRepository = new Mock<IUsersRepository>();
            mockUsersRepository.Setup(repo => repo.GetByLogin("admin")).Returns(
                user);
            var usersService = new UsersService(mockUsersRepository.Object, _mapper);

            usersService.Update(edit_user);

            mockUsersRepository.Verify(repo => repo.Update(It.IsAny<Users>()), Times.Once);
        }

        [Fact]
        public void TestGetAll()
        {
            var mockUsersRepository = new Mock<IUsersRepository>();
            var usersService = new UsersService(mockUsersRepository.Object, _mapper);

            usersService.GetAll();

            mockUsersRepository.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public void TestGetByLogin()
        {
            var mockUsersRepository = new Mock<IUsersRepository>();
            var usersService = new UsersService(mockUsersRepository.Object, _mapper);

            usersService.GetByLogin("admin");

            mockUsersRepository.Verify(repo => repo.GetByLogin(It.IsAny<string>()), Times.Once);
        }
    }
}
