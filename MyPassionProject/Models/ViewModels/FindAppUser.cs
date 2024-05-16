using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPassionProject.Models.ViewModels
{
    public class FindAppUser
    {
        //This is a class
        //public AppUserDto SelectedAppUser { get; set; }
        
        public IEnumerable<EventDto> PaticipatingEvents { get; set; }
    }
}