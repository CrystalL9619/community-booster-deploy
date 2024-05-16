using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPassionProject.Models
{
    public class Category
    {
        [Key] 
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        //A category has many events
        public ICollection<Event> Events { get; set;}

    }
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}