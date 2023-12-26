using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using db_cp.Interfaces;
using db_cp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using System.Reflection;
using db_cp.Services;
using Microsoft.AspNetCore.Http;
using db_cp.DTO;
using db_cp.ModelsBL;
using AutoMapper;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace db_cp.Controllers
{
    [ApiController]
    [Route("api/v1/account")]
    public class AccountController : ControllerBase
    {
        private readonly IUsersService userService;
        private readonly ILogger<AccountController> logger;
        private IMapper mapper;
        private readonly IMemoryCache memoryCache;

        public AccountController(IUsersService userService, ILogger<AccountController> logger, IMapper mapper, IMemoryCache memoryCache)
        {
            this.userService = userService;
            this.logger = logger;
            this.mapper = mapper;
            this.memoryCache = memoryCache;
        }

        private string GetHashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashedPasswordBytes = sha.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashedPasswordBytes);
            }
        }

        private string GenerateVerificationCode()
        {
            Random random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void SendVerificationCodeByEmail(string surname, string emailAddress, string verificationCode)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Tractor APP", "testapp111122220@gmail.com"));
                message.To.Add(new MailboxAddress(surname, emailAddress));
                message.Subject = "Verification Code";
                message.Body = new TextPart("plain")
                {
                    Text = $"Your verification code is: {verificationCode}"
                };
                
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.ethereal.email", 587, false);
                    client.Authenticate("arnoldo14@ethereal.email", Environment.GetEnvironmentVariable("mailPassword"));
                    client.Send(message);

                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
        }

        [HttpGet("test/verify/{userLogin}")]
        public IActionResult GetTestVerificationCode(string userLogin)
        {
            if (memoryCache.TryGetValue(userLogin + "_verificationCode", out string savedCode))
            {
                return Ok(savedCode);
            }

            return NotFound("Verification code not found for the user.");
        }

        [HttpGet("verify/{code}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> VerifyCode(string code)
        {
            var user = mapper.Map<NewUsersDTO>(userService.GetByLogin(Request.Cookies["userLogin"]));
            if (memoryCache.TryGetValue(user.Login + "_verificationCode", out string savedCode))
            {
                if (code == savedCode)
                {
                    await Authenticate(user);
                    return Ok("User authenticated successfully");
                }
            }

            return BadRequest("Invalid verification code");
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] UsersLoginDTO model)
        {
            var user = mapper.Map<NewUsersDTO>(userService.GetByLogin(model.Login));
            Response.Cookies.Append("userLogin", user.Login);

            if (user == null)
            {
                return NotFound();
            }

            string inputPassword = GetHashPassword(model.Password);

            if (user.Password == inputPassword)
            {
                string verificationCode = GenerateVerificationCode();
                SendVerificationCodeByEmail(user.Surname,user.Login, verificationCode);

                memoryCache.Set(user.Login + "_verificationCode", verificationCode, TimeSpan.FromMinutes(5));

                return Ok("Verification code sent to your email");
            }

            return BadRequest();
        }

        [HttpGet("logout")]
        [RoleDescription("for all", "")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok("User logged out successfully");
        }

        private async Task Authenticate(NewUsersDTO user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Permission)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
