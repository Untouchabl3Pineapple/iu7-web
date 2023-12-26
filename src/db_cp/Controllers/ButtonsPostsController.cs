using Microsoft.AspNetCore.Mvc;
using db_cp.Models;
using db_cp.Services;
using System.Collections.Generic;
using System;
using db_cp.DTO;
using System.Reflection;
using System.Data;
using AutoMapper;
using System.Diagnostics;

namespace db_cp.Controllers
{
    [ApiController]
    [Route("api/v1/buttonsposts")]
    public class ButtonsPostsController : ControllerBase
    {
        private readonly IButtonsPostsService buttonPostService;
        private IMapper mapper;

        public ButtonsPostsController(IButtonsPostsService buttonPostService, IMapper mapper)
        {
            this.buttonPostService = buttonPostService;
            this.mapper = mapper;
        }

        [HttpGet]
        [RoleDescription("for all", "")]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<ButtonsPostsDTO>> GetAll()
        {
            return Ok(mapper.Map<IEnumerable<ButtonsPostsDTO>>(buttonPostService.GetAll()));
        }
    }
}