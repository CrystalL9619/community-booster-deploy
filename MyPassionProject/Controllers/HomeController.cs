using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using MyPassionProject.Models;
using MyPassionProject.Models.ViewModels;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using System.Diagnostics;
using System.Security.Policy;

namespace MyPassionProject.Controllers
{
    public class HomeController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static HomeController()
        {
            client = new HttpClient();
            client.BaseAddress = new System.Uri("https://localhost:44317/api/");
        }
        public ActionResult Index()
        {
            //send request event data controller -> list event for category
            //use HTTP client to access infomation
            //objective: communicate with our event data api to retrieve a list of event
            // make two variable
            // one for EventData/ListEventsForCategory/1 event , one for EventData/ListEventsForCategory/2 event

            string url1 = "EventData/ListEventsForCategory/1";
            HttpResponseMessage response1 = client.GetAsync(url1).Result;
            List<EventDto> FunEvents = response1.Content.ReadAsAsync<List<EventDto>>().Result;

            string url2 = "EventData/ListEventsForCategory/2";
            HttpResponseMessage response2 = client.GetAsync(url2).Result;
            List<EventDto> FareEvents = response2.Content.ReadAsAsync<List<EventDto>>().Result;

            // Prepare view model
            var Index = new EventsForCategory
            {
                FunEvents = FunEvents,
                FareEvents = FareEvents
            };



            // return this view model to view

            return View(Index);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        // GET: Home/Account
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Account()//
        {
            //use HTTP client to access infomation
            //objective: communicate with our event data api to retrieve a list of event
            //curl https://localhost:44317/api/EventData/ListEventsForApplicationUser
            //string CurrentUserId = User.Identity.GetUserId();

            //string url = "EventData/ListEventsForApplicationUser?CurrentUserId={CurrentUserId}";
            //HttpResponseMessage response = client.GetAsync(url).Result;//According to your method, use GetAsync,PostAsync,or ReadAsAsync.
            //List<EventDto> Events = response.Content.ReadAsAsync<List<EventDto>>().Result;


            //return View(Events);

            string CurrentUserId = User.Identity.GetUserId();
            Debug.WriteLine("what is " + CurrentUserId);
            string url = $"EventData/ListEventsForApplicationUser?CurrentUserId={CurrentUserId}";
            Debug.WriteLine("It is going to" + url);
            HttpResponseMessage response = null;
            List<EventDto> Events = null;
            response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode(); // Ensure HTTP 200 OK status
            Events = response.Content.ReadAsAsync<List<EventDto>>().Result;

            Debug.WriteLine("Again, what is " + CurrentUserId);
            string listcreatedUrl = $"EventData/ListCreatedEventForUser?CurrentUserId={CurrentUserId}";
            Debug.WriteLine("It is going to" + listcreatedUrl);
            HttpResponseMessage listcreatedUrlResponse = client.GetAsync(listcreatedUrl).Result;
            List<EventDto> CreatedEvents = listcreatedUrlResponse.Content.ReadAsAsync<List<EventDto>>().Result;

          
            var findEvent = new FindEvent
            {
                CurrentUserId = CurrentUserId,
                RelatedEvent = Events,
                CreatedEvents = CreatedEvents
            };
            return View(findEvent);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
            public ActionResult Associate(int EventId, string CurrentUserId)
            {

                string convertedEventId = EventId.ToString();


                string associateUrl = "EventData/AssociateEventWithApplicationUser/" + convertedEventId + "/" + CurrentUserId;
                HttpContent content = new StringContent("");
                content.Headers.ContentType.MediaType = "application/json";
                HttpResponseMessage associateResponse = client.PostAsync(associateUrl, content).Result;

                return RedirectToAction("Account");
            }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult UnAssociate(int EventId, string CurrentUserId)
        {
            string convertedEventId = EventId.ToString();

                string unassociateUrl = "EventData/UnAssociateEventWithApplicationUser/" + convertedEventId + "/" + CurrentUserId;

                HttpContent content = new StringContent("");
                content.Headers.ContentType.MediaType = "application/json";
                HttpResponseMessage unassociateResponse = client.PostAsync(unassociateUrl, content).Result;

                return RedirectToAction("Account");
        }
        [HttpGet]
        public ActionResult Search(string query)
        {
            Debug.WriteLine("Attempt to Search");
            string url = $"EventData/SearchEvent?query={query}";

            try
            {
                HttpResponseMessage response = client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode(); // Ensure that the HTTP request was successful
                List<EventDto> RelatedEvent = response.Content.ReadAsAsync<List<EventDto>>().Result;

                // Pass the CreatedEvents list to the view for rendering
                return View("Search", RelatedEvent);
            }
            catch (HttpRequestException ex)
            {
                // Log the exception or handle it appropriately
                Debug.WriteLine($"HTTP request failed: {ex.Message}");
                return View("Error");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Debug.WriteLine($"An error occurred: {ex.Message}");
                return View("Error");
            }
        }




    }
}