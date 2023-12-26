using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using TestsCore.Builders;
using Xunit;
using db_cp.Models;
using TestsCore.ObjectMothers;
using db_cp.DTO;
using System.Collections.Generic;
using DnsClient;
using System.Xml.Linq;
using TechTalk.SpecFlow;
using Microsoft.Extensions.Caching.Memory;
//using NUnit.Framework;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using Microsoft.AspNetCore.Http;
using Swashbuckle.Swagger;
using Microsoft.VisualStudio.TestPlatform.Common;
using db_cp.Controllers;

namespace E2ETests
{
    [Binding]
    public class E2E : IClassFixture<WebFactory>
    {
        private HttpResponseMessage authResponse;
        private UsersLoginDTO userLoginData;
        private string verifyCode;
        private HttpResponseMessage verifyResponse;

        private readonly ScenarioContext _scenarioContext;
        private readonly WebFactory _factory;
        private readonly HttpClient client;
        private readonly AppDBContext context;
        private readonly bool skip = Environment.GetEnvironmentVariable("skip") == "true";

        public E2E(WebFactory factory, ScenarioContext scenarioContext)
        {
            // Deploy
            string connString = "Server=postgres;User ID=postgres;Password=17009839;Port=5432;Database=tractor_plant;";
            // Local
            //string connString = "Server=localhost;User ID=postgres;Password=17009839;Port=5433;Database=tractor_plant;";

            _scenarioContext = scenarioContext;
            _factory = factory;
            client = factory.CreateClient();
            context = new AppDBContext(new DbContextOptionsBuilder<AppDBContext>()
                                                .UseNpgsql(connString)
                                                .Options);
            addUser("admin@gmail.com", Environment.GetEnvironmentVariable("userPassword"), "admin");
        }

        ~E2E()
        {
            ClearDb();
        }

        private void ClearDb()
        {
            context.EventsTypes.RemoveRange(context.EventsTypes);
            context.Users.RemoveRange(context.Users);
            context.Events.RemoveRange(context.Events);
            context.ButtonsEvents.RemoveRange(context.ButtonsEvents);
            context.ButtonsPosts.RemoveRange(context.ButtonsPosts);
            context.SaveChanges();
        }

        // Чтобы так не делать, буду использовать api
        //var builder = new UsersOM().CreateUsers().withPassword("jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg="); // cringe)
        //var user = builder.build();

        //context.Users.Add(user);
        //context.SaveChanges();

        private void addUser(string login, string password, string permission) // Закрытая система, завести пользователя без админки нельзя
        {
            var userData = new NewUsersDTO // simple data
            {
                Login = login,
                Password = password,
                Permission = permission,
                Name = "user",
                Surname = "user",
                MiddleName = "user"
            };

            var regResponse = client.PostAsync("/api/v1/users/register", JsonContent.Create(userData)).Result;
        }

        [Given("пользователь вводит данные для входа")]
        public void authGiven()
        {
            userLoginData = new UsersLoginDTO { Login = "admin@gmail.com", Password = "admin" };
        }

        [When("пользователь отправляет запрос на вход в систему с учетными данными")]
        public void authWhen()
        {
            authResponse = client.PostAsync("/api/v1/account/login", JsonContent.Create(userLoginData)).Result;
        }

        [Then("система должна вернуть успешный ответ и отправить код верификации на почту")]
        public void authThen()
        {
            Assert.Equal(HttpStatusCode.OK, authResponse.StatusCode);
        }

        [Given("пользователь вводит полученный код верификации")]
        public void verifyGiven()
        {
            verifyCode = client.GetAsync("/api/v1/account/test/verify/" + "admin@gmail.com").Result.Content.ReadAsStringAsync().Result;
        }

        [When("пользователь отправляет запрос на подтверждение кода верификации")]
        public void verifyWhen()
        {
            verifyResponse = client.GetAsync("/api/v1/account/verify/" + verifyCode).Result;
        }

        [Then("система должна вернуть успешный ответ и пользователь авторизуется")]
        public void verifyThen()
        {
            Assert.Equal(HttpStatusCode.OK, verifyResponse.StatusCode);
        }
    }
}