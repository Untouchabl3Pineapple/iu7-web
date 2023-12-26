using Microsoft.AspNetCore.Mvc;
using db_cp.Models;
using db_cp.Services;
using System.Collections.Generic;
using System;
using db_cp.DTO;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using db_cp.ModelsBL;

namespace db_cp.Controllers
{
    //[Authorize(Roles = "admin, foreman, shop manager")]
    [ApiController]
    [Route("api/v1/buttonsevents")]
    public class ButtonsEventsController : ControllerBase
    {
        private readonly IButtonsEventsService buttonEventService;
        private IMapper mapper;

        public ButtonsEventsController(IButtonsEventsService buttonEventService, IMapper mapper)
        {
            this.buttonEventService = buttonEventService;
            this.mapper = mapper;
        }

        [HttpGet]
        [RoleDescription("admin, foreman, shop manager", "")]
        [ProducesResponseType(200)]
        public ActionResult<ButtonsEventsDTO> GetAll()
        {
            return Ok(mapper.Map<IEnumerable<ButtonsEventsDTO>>(buttonEventService.GetAll()));
        }

        [HttpGet("{id}")]
        [RoleDescription("admin, foreman, shop manager", "")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<ButtonsEventsDTO> Get(int id)
        {
            var buttonEvent = buttonEventService.GetByID(id);
            return buttonEvent != null ? Ok(mapper.Map<ButtonsEventsDTO>(buttonEvent)) : NotFound();
        }

        [HttpPost]
        [RoleDescription("admin, foreman, shop manager", "")]
        [ProducesResponseType(201, Type = typeof(ButtonsEventsDTO))]
        [ProducesResponseType(400)]
        public IActionResult CreateButtonEvent([FromBody] ButtonsEventsBaseDTO modelDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var addedButtonEvent = buttonEventService.Add(mapper.Map<ButtonsEventsBL>(modelDTO));
                    return Ok(mapper.Map<ButtonsEventsDTO>(addedButtonEvent));
                }
                catch (Exception ex)
                {
                    //logger.LogInformation(User.Identity.Name, MethodBase.GetCurrentMethod().Name, ex.Message);

                    return BadRequest();
                }
            }
            else
                return BadRequest();
        }

    }
}
