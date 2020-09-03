using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGSTwitter.Services;
using TimetableManager.Helpers.Interfaces;
using TimetableManager.Helpers.Models;

namespace TimetableManager.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EventsController : ControllerBase
    {
        private readonly IEventsService _eventsService;
        public EventsController(IEventsService eventsService)
        {
            _eventsService = eventsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _eventsService.GetEvents();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var eventToReturn = await _eventsService.GetEvent(id);
            if (eventToReturn == null)
                return NotFound();
            return Ok(eventToReturn);
        }

        [HttpPost("new")]
        public async Task<IActionResult> CreateEvent(EventDto eventToCreate)
        {
            var createdEvent = await _eventsService.CreateEvent(eventToCreate);
            return CreatedAtAction(nameof(GetEvent),
                new { id = createdEvent.Id },
                createdEvent);
        }

        [HttpPost("newRoutine")]
        public async Task<IActionResult> CreateRoutineEvents(EventDto eventsToCreate)
        {
            var serviceStatus = await _eventsService.CreateRoutineEvents(eventsToCreate);
            return serviceStatus switch
            {
                ServiceStatus.Success => NoContent(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, EventDto updatedEvent)
        {
            var serviceStatus = await _eventsService.UpdateEvent(id, updatedEvent);

            return serviceStatus switch
            {
                ServiceStatus.Success => NoContent(),
                ServiceStatus.NotFound => BadRequest(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent([FromRoute]int id)
        {
            var serviceStatus = await _eventsService.DeleteEvent(id);
            return serviceStatus switch
            {
                ServiceStatus.Success => NoContent(),
                ServiceStatus.NotFound => BadRequest(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllEvents()
        {
            var serviceStatus = await _eventsService.DeleteAllEvents();
            return serviceStatus switch
            {
                ServiceStatus.Success => NoContent(),
                ServiceStatus.NotFound => NotFound(),
                _ => throw new ArgumentNullException()
            };
        }

        [HttpGet("getId")]
        public async Task<IActionResult> GetLatestEventId()
        {
            var id = await _eventsService.GetLatestEventId();
            return Ok(id);
        }

    }
}
