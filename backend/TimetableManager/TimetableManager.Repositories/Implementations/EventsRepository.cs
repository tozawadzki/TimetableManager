using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimetableManager.App;
using TimetableManager.Repositories.Interfaces;
using TimetableManager.Repositories.Models;

namespace TimetableManager.Repositories.Implementations
{
    public class EventsRepository : IEventsRepository
    {
        private readonly ApplicationDbContext _context;

        public EventsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            var events = await _context.Events.ToListAsync();
            return events;
        }

        public async Task<Event> GetEvent(int eventId)
        {
            var singleEvent = await _context.Events.Where(x => x.Id == eventId).SingleOrDefaultAsync();
            return singleEvent;
        }

        public async Task DeleteEvent(int eventId)
        {
            var eventToDelete = await _context.Events.FindAsync(eventId);
            if (eventToDelete != null)
                _context.Events.Remove(eventToDelete);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateEvent(int eventId, Event updatedEvent)
        {
            var eventToUpdate = await _context.Events.FindAsync(eventId);
            if (eventToUpdate != null)
            {
                eventToUpdate.Title = updatedEvent.Title;
                eventToUpdate.Start = updatedEvent.Start; 
                eventToUpdate.End = updatedEvent.End;
                eventToUpdate.AllDay = updatedEvent.AllDay;
                eventToUpdate.Color = updatedEvent.Color;
            }

            await _context.SaveChangesAsync();
        }

        public async Task CreateEvent(Event newEvent)
        {
            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();
        }
    }
}
