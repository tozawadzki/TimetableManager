using System.Threading.Tasks;
using TimetableManager.Helpers.Models;

namespace TimetableManager.Helpers.Interfaces
{
    public interface IAutomationService
    {
        Task<int> GetSpentHours(string color);
        Task<GroupHours> GetSpentHours(ReportInterval interval);
    }
}
