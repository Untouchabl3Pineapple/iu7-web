using Microsoft.AspNetCore.Mvc;
using db_cp.Interfaces;
using db_cp.Models;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Text;
using System.Security.Cryptography;
using db_cp.Services;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using db_cp.DTO;
using System.Threading.Tasks;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using db_cp.ModelsBL;

namespace db_cp.Controllers
{
    //[Authorize(Roles = "admin")]
    [ApiController]
    [Route("api/v1/users")]
    public class UsersController : ControllerBase
    {
        private IUsersService userService;
        private readonly ILogger<UsersController> logger;
        private IMapper mapper;

        public UsersController(IUsersService userService, ILogger<UsersController> logger, IMapper mapper)
        {
            this.userService = userService;
            this.logger = logger;
            this.mapper = mapper;
        }

        private string GetHashPassword(string password)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(password);
            var hashedPassword = sha.ComputeHash(asByteArray);

            return Convert.ToBase64String(hashedPassword);
        }

        [HttpGet]
        [RoleDescription("admin", "")]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<UsersDTO>> GetAll()
        {
            logger.LogInformation(User.Identity.Name, MethodBase.GetCurrentMethod().Name);
            return Ok(mapper.Map<IEnumerable<UsersDTO>>(userService.GetAll()));
        }

        [HttpGet("{login}")]
        [RoleDescription("admin", "")]
        [ProducesResponseType(200, Type = typeof(UsersDTO))]
        [ProducesResponseType(404)]
        public IActionResult GetUser(string login)
        {
            var user = userService.GetByLogin(login);
            return user != null ? Ok(mapper.Map<UsersDTO>(user)) : NotFound();
        }

        [HttpDelete("{login}")]
        [RoleDescription("admin", "")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(string login)
        {
            var deletedUser = userService.Delete(login);
            return deletedUser != null ? NoContent() : NotFound();
        }

        [HttpPost("register")]
        [RoleDescription("admin", "")]
        [ProducesResponseType(201, Type = typeof(UsersDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public IActionResult CreateUser([FromBody] NewUsersDTO modelDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    modelDTO.Password = GetHashPassword(modelDTO.Password);

                    logger.LogInformation(User.Identity.Name, MethodBase.GetCurrentMethod().Name);

                    var addedUser = userService.Add(mapper.Map<UsersBL>(modelDTO));
                    return Ok(mapper.Map<UsersDTO>(addedUser));
                }
                catch (Exception ex)
                {
                    logger.LogInformation(User.Identity.Name, MethodBase.GetCurrentMethod().Name, ex.Message);

                    return Conflict();
                }
            }
            else
                return BadRequest();
        }

        [HttpPut("{login}")]
        [RoleDescription("admin", "")]
        [ProducesResponseType(200, Type = typeof(UsersDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public IActionResult UpdateUser(string login, [FromBody] UsersUpdatePermDTO modelDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = mapper.Map<NewUsersDTO>(userService.GetByLogin(login));
                    if (user == null)
                    {
                        return NotFound();
                    }
                    user.Permission = modelDTO.Permission;
                    userService.Update(mapper.Map<UsersBL>(user));
                    return Ok(user);
                }
                catch (Exception ex)
                {
                    logger.LogInformation(User.Identity.Name, MethodBase.GetCurrentMethod().Name, ex.Message);

                    return Conflict();
                }
            }
            else
                return BadRequest();
        }
    }
}
