using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using db_cp.Services;
using db_cp.Models;
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using db_cp.DTO;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using db_cp.ModelsBL;

namespace db_cp.Controllers
{
    //[Authorize(Roles = "admin, shop manager")]
    [ApiController]
    [Route("api/v1/eventstypes")]
    public class EventsTypesController : ControllerBase
    {
        private readonly IEventsTypesService eventTypeService;
        private readonly ILogger<EventsTypesController> logger;
        private IMapper mapper;

        public EventsTypesController(IEventsTypesService eventTypeService, ILogger<EventsTypesController> logger, IMapper mapper)
        {
            this.eventTypeService = eventTypeService;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        [RoleDescription("admin, shop manager", "")]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<EventsTypes>> GetAll()
        {
            return Ok(mapper.Map<IEnumerable<EventsTypesDTO>>(eventTypeService.GetAll()));
        }

        [HttpGet("{id}")]
        [RoleDescription("admin, shop manager", "")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<EventsTypes> Get(int id)
        {
            var eventType = eventTypeService.GetByID(id);
            return eventType != null ? Ok(mapper.Map<EventsTypesDTO>(eventType)) : NotFound();
        }

        [HttpDelete("{id}")]
        [RoleDescription("admin, shop manager", "")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult<string> DeleteEventType(int id)
        {
            var deletedEventType = eventTypeService.Delete(id);
            return deletedEventType != null ? NoContent() : NotFound();
        }

        [HttpPost]
        [RoleDescription("admin, shop manager", "")]
        [ProducesResponseType(201, Type = typeof(EventsTypesDTO))]
        [ProducesResponseType(400)]
        public ActionResult<string> AddEventType([FromBody] EventsTypesBaseDTO modelDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var addedEventType = eventTypeService.Add(mapper.Map<EventsTypesBL>(modelDTO));
                    return Ok(mapper.Map<EventsTypesDTO>(addedEventType));
                }
                catch (Exception ex)
                {
                    logger.LogInformation(User.Identity.Name, MethodBase.GetCurrentMethod().Name, ex.Message);
                    //return Conflict();
                }
            }
            return BadRequest();
        }
    }
}
