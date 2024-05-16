using System.Collections.Generic;
using System.Web;

namespace MyPassionProject.Models.ViewModels
{
    public class FindCategory
    {
        //This is a class
        //the Category itself that we want to display
        public CategoryDto SelectedCategory { get; set; }

        public IEnumerable<CategoryDto> CategoryOptions { get; set; }

        //all of the related Events to that particular Category
        public IEnumerable<EventDto> RelatedEvents { get; set; }
        public string CreatorId { get; set; }

    }
}
