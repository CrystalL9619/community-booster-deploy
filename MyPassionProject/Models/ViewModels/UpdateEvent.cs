using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPassionProject.Models.ViewModels
{
    public class UpdateEvent
    {
        //This viewmodel is a class which stores information that we need to present to /Event/Update/{}

        //the existing Event information

        public EventDto SelectedEvent { get; set; }

        // all Categories to choose from when updating this event

        public IEnumerable<CategoryDto> CategoryOptions { get; set; }
    }
}