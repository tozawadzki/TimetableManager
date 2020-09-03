using System.Linq;
using System.Threading.Tasks;
using TimetableManager.Helpers.Interfaces;
using TimetableManager.Helpers.Models;
using TimetableManager.Repositories.Interfaces;

namespace TimetableManager.Helpers.Services
{
    public class AutomationService : IAutomationService
    {
        private readonly IEventsRepository _eventsRepository;

        public AutomationService(IEventsRepository eventsRepository)
        {
            _eventsRepository = eventsRepository;
        }
        public async Task<int> GetSpentHours(string color)
        {
            int hours = 0;
            var events = await _eventsRepository.GetEvents();

            var groupedEvents = events.Where(x => x.Color == color).ToList();

            foreach (var groupEvent in groupedEvents)
            {
                if (groupEvent.Start.HasValue)
                {
                    var start = groupEvent.Start.Value.Hour;
                    var end = groupEvent.End.Value.Hour;
                    var hoursToAdd = end - start;
                    hours += hoursToAdd;
                }
            }
            return hours;
        }

        public async Task<GroupHours> GetSpentHours(ReportInterval interval)
        {
            var groupHours = new GroupHours();
            var from = interval.Start;
            var to = interval.End;
            var events = await _eventsRepository.GetEvents();
            var eventsInRange = events.Where(x => x.Start >= from && x.End <= to);

            foreach (var e in eventsInRange)
            {
                int hours = 0;
                var start = e.Start.Value.Hour;
                var end = e.End.Value.Hour;
                var hoursToAdd = end - start;
                hours += hoursToAdd;

                switch (e.Color)
                {
                    case ("blue"):
                        groupHours.School += hours;
                        break;
                    case ("green"):
                        groupHours.Work += hours;
                        break;
                    case ("red"):
                        groupHours.Workout += hours;
                        break;
                    case ("black"):
                        groupHours.SelfImprovement += hours;
                        break;
                    case ("yellow"):
                        groupHours.Entertainment += hours;
                        break;
                }
            }
            return groupHours;
        }
    }
}
