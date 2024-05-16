using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;


namespace MyPassionProject.Models
{
    public class Event
    {
        [Key] 
        public int EventId { get; set; }
        [Column(TypeName = "datetime2")]

        public DateTime UpdateDate { get; set; }
        public string Title {  get; set; }
        public string Location { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime EventDateTime { get; set; }

        public string Capacity { get; set; }
        public string Details { get; set; }
     
        [AllowHtml]
        public string ImagePath { get; set; }

        //Many AppUsers involved in an event
        //public ICollection<AppUser> AppUsers { get; set; }

        public ICollection<ApplicationUser> ApplicationUser { get; set; }
        public string CreatorId { get; set; }

        //A event belongs to one category
        //A category has many events

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

    }
    public class EventDto
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime EventDateTime { get; set; }

        public string Capacity { get; set; }
        public string Details { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public string ImagePath { get; set; }
        public string CreatorId { get; set; }
    }
}