using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPassionProject.Models.ViewModels
{
    public class EventsForCategory
    {
        public IEnumerable<EventDto> FunEvents { get; set; }
        public IEnumerable<EventDto> FareEvents { get; set; }
    }
}