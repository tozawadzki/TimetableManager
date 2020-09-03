using System;
using System.Collections.Generic;

namespace TimetableManager.Helpers.Models
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool AllDay { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public List<int> DaysOfWeek { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Color { get; set; }
    }
}
