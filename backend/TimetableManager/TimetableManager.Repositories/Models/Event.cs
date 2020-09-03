using System;
using System.ComponentModel.DataAnnotations;

namespace TimetableManager.Repositories.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public bool AllDay { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string Color { get; set; }
    }
}
