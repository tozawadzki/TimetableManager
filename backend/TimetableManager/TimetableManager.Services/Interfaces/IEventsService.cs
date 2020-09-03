using System.Collections.Generic;
using System.Threading.Tasks;
using PGSTwitter.Services;
using TimetableManager.Helpers.Models;

namespace TimetableManager.Helpers.Interfaces
{
    public interface IEventsService
    {
        Task<EventDto> GetEvent(int id);
        Task<EventDto> CreateEvent(EventDto newEvent);
        Task<List<EventDto>> GetEvents();
        Task<ServiceStatus> CreateRoutineEvents(EventDto newEvent);
        Task<ServiceStatus> UpdateEvent(int id, EventDto updatedEvent);
        Task<ServiceStatus> DeleteEvent(int id);
        Task<ServiceStatus> DeleteAllEvents();
        Task<int> GetLatestEventId();
    }
}
