using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PGSTwitter.Services;
using TimetableManager.Helpers.Interfaces;
using TimetableManager.Helpers.Models;
using TimetableManager.Repositories.Interfaces;
using TimetableManager.Repositories.Models;

namespace TimetableManager.Helpers.Services
{
    public class EventsService : IEventsService
    {
        private readonly IEventsRepository _eventsRepository;
        private readonly IMapper _mapper;

        public EventsService(IEventsRepository eventsRepository, IMapper mapper)
        {
            _eventsRepository = eventsRepository;
            _mapper = mapper;
        }

        public async Task<List<EventDto>> GetEvents()
        {
            var events = await _eventsRepository.GetEvents();
            var eventDtos = events.Select(e => _mapper.Map<Event, EventDto>(e)).OrderBy(x => x.Start).ToList();

            return eventDtos;
        }

        public async Task<ServiceStatus> CreateRoutineEvents(EventDto newEvent)
        {
            var daysOfWeek = newEvent.DaysOfWeek;
            if (daysOfWeek != null)
            {
                foreach (var day in newEvent.DaysOfWeek)
                {
                    switch (day)
                    {
                        case (1):
                            await CreateRoutineEvent(newEvent, DayOfWeek.Monday);
                            break;
                        case (2):
                            await CreateRoutineEvent(newEvent, DayOfWeek.Tuesday);
                            break;
                        case (3):
                            await CreateRoutineEvent(newEvent, DayOfWeek.Wednesday);
                            break;
                        case (4):
                            await CreateRoutineEvent(newEvent, DayOfWeek.Thursday);
                            break;
                        case (5):
                            await CreateRoutineEvent(newEvent, DayOfWeek.Friday);
                            break;
                    }
                }
            }
            return ServiceStatus.Success;
        }

        public async Task CreateRoutineEvent(EventDto newEvent, DayOfWeek dayOfWeek)
        {
            var helperDate = DateTime.Now;
            for (int m = helperDate.Month; m <= 12; m++)
            {
                var daysInMonth = DateTime.DaysInMonth(helperDate.Year, m);
                for (int d = helperDate.Day; d <= daysInMonth; d++)
                {

                    while (helperDate.DayOfWeek != dayOfWeek)
                    {
                        if (d <= daysInMonth)
                        {
                            helperDate = helperDate.AddDays(1);
                            d++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (d <= daysInMonth)
                    {
                        try
                        {
                            var startTime = DateTime.Parse(newEvent.StartTime);
                            var endTime = DateTime.Parse(newEvent.EndTime);
                            var recurringEventStartTime = new DateTime(helperDate.Year, m, helperDate.Day, startTime.Hour,
                                startTime.Minute, startTime.Second);
                            var recurringEventEndTime = new DateTime(helperDate.Year, m, helperDate.Day, endTime.Hour,
                                endTime.Minute, endTime.Second);
                            newEvent.Start = recurringEventStartTime;
                            newEvent.End = recurringEventEndTime;
                            await CreateEvent(newEvent);
                            helperDate = helperDate.AddDays(1);
                        }

                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            //serilog
                        }
                    }
                   
                }

            }
        }
        public async Task<EventDto> CreateEvent(EventDto newEvent)
        {
            var eventToAdd = _mapper.Map<EventDto, Event>(newEvent);
            await _eventsRepository.CreateEvent(eventToAdd);
            var eventDto = _mapper.Map<Event, EventDto>(eventToAdd);
            return eventDto;
        }

        public async Task<ServiceStatus> UpdateEvent(int id, EventDto updatedEvent)
        {
            var eventToUpdate = await _eventsRepository.GetEvent(id);
            if (eventToUpdate == null)
                return ServiceStatus.NotFound;

            eventToUpdate = _mapper.Map<EventDto, Event>(updatedEvent);
            await _eventsRepository.UpdateEvent(id, eventToUpdate);

            return ServiceStatus.Success;
        }

        public async Task<ServiceStatus> DeleteEvent(int id)
        {
            var eventToDelete = await _eventsRepository.GetEvent(id);
            if (eventToDelete == null)
                return ServiceStatus.NotFound;

            await _eventsRepository.DeleteEvent(eventToDelete.Id);

            return ServiceStatus.Success;
        }

        public async Task<ServiceStatus> DeleteAllEvents()
        {
            var events = await _eventsRepository.GetEvents();
            var identificators = events.Select(x => x.Id);
            foreach (int id in identificators)
            {
                try
                {
                    await DeleteEvent(id);
                }
                catch
                {
                    return ServiceStatus.NotFound;
                }
            }

            return ServiceStatus.Success;
        }

        public async Task<EventDto> GetEvent(int id)
        {
            var eventToReturn = await _eventsRepository.GetEvent(id);
            if (eventToReturn == null)
                return null;

            var eventDto = _mapper.Map<Event, EventDto>(eventToReturn);

            return eventDto;
        }

        public async Task<int> GetLatestEventId()
        {
            var events = await _eventsRepository.GetEvents();
            if (!events.Any())
                return 1;

            var latestId = events.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault() + 1;
            return latestId;
        }


    }
}
