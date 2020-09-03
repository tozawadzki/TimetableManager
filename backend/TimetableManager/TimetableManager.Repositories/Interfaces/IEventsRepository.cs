using System.Collections.Generic;
using System.Threading.Tasks;
using TimetableManager.Repositories.Models;

namespace TimetableManager.Repositories.Interfaces
{
    public interface IEventsRepository
    {
        Task<IEnumerable<Event>> GetEvents();
        Task<Event> GetEvent(int eventId);
        Task DeleteEvent(int eventId);
        Task UpdateEvent(int eventId, Event updatedEvent);
        Task CreateEvent(Event newEvent);
    }
}
