using Microsoft.AspNetCore.Mvc;
using db_cp.Models;
using db_cp.Services;
using System.Collections.Generic;
using System;
using db_cp.DTO;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using db_cp.ModelsBL;
using DnsClient;

namespace db_cp.Controllers
{
    //[Authorize(Roles = "admin, foreman, shop manager")]
    [ApiController]
    [Route("api/v1/events")]
    public class EventsController : ControllerBase
    {
        private readonly IEventsService eventService;
        private readonly IEventsTypesService eventTypeService;
        private IMapper mapper;

        public EventsController(IEventsService eventService, IEventsTypesService eventTypeService, IMapper mapper)
        {
            this.eventService = eventService;
            this.eventTypeService = eventTypeService;
            this.mapper = mapper;
        }

        [HttpGet]
        [RoleDescription("admin, foreman, shop manager", "")]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<EventsDTO>> Get()
        {
            return Ok(mapper.Map<IEnumerable<EventsDTO>>(eventService.GetAll()));
        }

        [HttpGet("{id}")]
        [RoleDescription("admin, foreman, shop manager", "")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<EventsDTO> Get(int id)
        {
            var eevent = eventService.GetByID(id);
            return eevent != null ? Ok(mapper.Map<EventsDTO>(eevent)) : NotFound();
        }

        [HttpDelete("{id}")]
        [RoleDescription("admin, foreman, shop manager", "")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult Delete(int id)
        {
            var deletedEvent = eventService.Delete(id);
            return deletedEvent != null ? NoContent() : NotFound();
        }

        [HttpPut("{id}")]
        [RoleDescription("admin, foreman, shop manager", "")]
        [ProducesResponseType(200, Type = typeof(EventsDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public ActionResult Put(int id, [FromBody] EventsUpdateDTO modelDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var eevent = mapper.Map<EventsDTO>(eventService.GetByID(id));
                    if (eevent == null)
                    {
                        return NotFound();
                    }
                    eevent.EventType_ID = modelDTO.EventType_ID;
                    eevent.EventDescription = modelDTO.EventDescription;

                    eventService.Update(mapper.Map<EventsBL>(eevent));
                    return Ok(eevent);
                }
                catch (Exception ex)
                {
                    return Conflict();
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
