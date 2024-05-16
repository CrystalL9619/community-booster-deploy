using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;//should add this to utilize the HttpClient
using System.Diagnostics;
using MyPassionProject.Models;//should add this to use EventDto 
using MyPassionProject.Models.ViewModels;
using System.Web.Script.Serialization;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using System;
using Microsoft.AspNet.Identity;
using System.Globalization;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;




namespace MyPassionProject.Controllers
{
    public class EventController : Controller  
    {
            private static readonly HttpClient client;
            private JavaScriptSerializer jss = new JavaScriptSerializer();

            static EventController()
            { 
            client = new HttpClient();
                client.BaseAddress = new System.Uri("https://localhost:44317/api/");
            }

        
        // GET: Event/List
        public ActionResult List()//should be the same as viewbag.title 
        {
            //use HTTP client to access infomation
            //objective: communicate with our event data api to retrieve a list of event
            //curl https://localhost:44317/api/EventData/ListEvents

            string url = "EventData/ListEvents";// In order to work , need a router like:" https://localhost:44317/api/"before string
            HttpResponseMessage response = client.GetAsync(url).Result;//According to your method, use GetAsync,PostAsync,or ReadAsAsync.
            List<EventDto> Events = response.Content.ReadAsAsync<List<EventDto>>().Result;

            //Views/Event/List.cshtml
            return View(Events);
        }


        //Get:Event/Find/2
        public ActionResult Find(int id)
        {
            //use HTTP client to access infomation
            //objective: communicate with our event data api to retrieve a specific event by ID
            //curl https://localhost:44317/api/EventData/FindEvent/2
            
            FindEvent ViewModel = new FindEvent();
             
            string convertedId = id.ToString();//super important!!!!
            string url = "EventData/FindEvent/" + convertedId;//In order to work , need a router like:"https://localhost:44317/api/"before string

            HttpResponseMessage response = client.GetAsync(url).Result;
            
            EventDto SelectedEvent = response.Content.ReadAsAsync<EventDto>().Result;

            Debug.WriteLine("event received : ");
            Debug.WriteLine(SelectedEvent.Title);

            ViewModel.SelectedEvent = SelectedEvent;

            //show associated ApplicationUsers with this Event
            /*
              url = "ApplicationUserData/ListApplicationUsersForEvent/" + convertedId;
              response = client.GetAsync(url).Result;

              IEnumerable<ApplicationUser> ParticipatingUsers = response.Content.ReadAsAsync<List<ApplicationUser>>().Result;

              ViewModel.ParticipatingUsers = ParticipatingUsers;



              url = "ApplicationUserData/ListApplicationUserNotForEvent/" + convertedId;

              response = client.GetAsync(url).Result;

              IEnumerable<ApplicationUser> NotPaticipatingUsers = response.Content.ReadAsAsync<IEnumerable<ApplicationUser>>().Result;

              ViewModel.NotPaticipatingUsers = NotPaticipatingUsers;
              */

            // Get current user
            // Current user object pass into view model
            string currentUserId = User.Identity.GetUserId();
            ViewModel.CurrentUserId = currentUserId;

            return View(ViewModel);
        }


        public ActionResult Error()
        {

            return View();
        }


        //POST: Event/Associate/{EventId}/{UserId}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Associate(int EventId, string CurrentUserId)
        {

            Debug.WriteLine("Attempting to associate event:" + EventId + " with ApplicationUser " + CurrentUserId);

            //call our api to associate event with ApplicationUser
            string convertedEventId = EventId.ToString();
            //string convertedUserId = CurrentUserId.ToString();

            string url = "EventData/AssociateEventWithApplicationUser/" + convertedEventId + "/" + CurrentUserId;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Find/" + EventId);
           
        }


        //Get: Event/UnAssociate/{EventId}?UserId={UserId}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult UnAssociate(int EventId, string CurrentUserId)
        {
            Debug.WriteLine("Unassociate " + EventId + "with" + CurrentUserId);
            string convertedEventId = EventId.ToString();
            //string convertedUserId = UserId.ToString();

            string url = "EventData/UnAssociateEventWithApplicationUser/" + convertedEventId + "/" + CurrentUserId;

            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;


             return RedirectToAction("Find/" + EventId);
           
        }


        // GET: Event/New
        public ActionResult New()
        {
            //infomation about all categories in the system
            //GET api/CategoryData/ListCategories
            FindCategory ViewModel = new FindCategory();
            string url = "CategoryData/ListCategories";
            HttpResponseMessage response = client.GetAsync(url).Result;
             
            IEnumerable<CategoryDto> CategoryOptions = response.Content.ReadAsAsync<List<CategoryDto>>().Result;
            ViewModel.CategoryOptions = CategoryOptions;

            string currentUserId = User.Identity.GetUserId();
            ViewModel.CreatorId = currentUserId;
            return View(ViewModel);
        }


        // POST: Event/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Event newEvent, HttpPostedFileBase EventImage)
        {
            Debug.WriteLine("the json payload is :");
            //Debug.WriteLine(Event.Title);
            //objective: add a new event into our system using the API
            //curl -H "Content-Type:application/json" -d @newEvent.json https://localhost:44317/api/EventData/AddEvent 
              
            string url = "EventData/AddEvent";//In order to work , need a router like:"https://localhost:44317/api/"before string

            string jsonpayload = jss.Serialize(newEvent);
            HttpContent jsoncontent = new StringContent(jsonpayload);
            jsoncontent.Headers.ContentType.MediaType = "application/json";
            Debug.WriteLine(jsonpayload);
  


            HttpResponseMessage response = client.PostAsync(url, jsoncontent).Result;
            Debug.WriteLine(response);

            //update request is successful, and we have image data
            if (response.IsSuccessStatusCode && EventImage != null)
            {
                
                // Read the response content as a string
                var responseData = response.Content.ReadAsAsync<Event>().Result;

                // Deserialize the response to get the ID
                var eventId = responseData.EventId;

                Console.WriteLine($"Event added successfully. ID: {eventId}");
                //Updating the animal picture as a separate request
                //Debug.WriteLine("Calling Add Image method.");
                //Send over image data for player
                url = "EventData/UploadEventImage/" + eventId;
                //Debug.WriteLine("Received  Picture "+Event.FileName);

                MultipartFormDataContent formData = new MultipartFormDataContent();
                HttpContent imagecontent = new StreamContent(EventImage.InputStream);
                formData.Add(imagecontent, "EventPic", EventImage.FileName);

                try
                {
                    response = client.PostAsync(url, formData).Result;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                

                return RedirectToAction("List");
            }
            else if (response.IsSuccessStatusCode)
            {
                //No image upload, but update still successful
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }


        // GET: Event/Edit/9
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            //grab the event information
            UpdateEvent ViewModel=new UpdateEvent();

            //objective: communicate with our event data api to retrieve a specific event by ID
            //curl https://localhost:44317/api/EventData/FindEvent/9

            string convertedId = id.ToString();//super important!!!!
            string url = "EventData/FindEvent/" + convertedId; //In order to work, need a router like: "https://localhost:44317/api/"before string
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            EventDto SelectedEvent = response.Content.ReadAsAsync<EventDto>().Result;
            
            ViewModel.SelectedEvent = SelectedEvent;

            // all categorys to choose from when updating this event
            //the existing event information
            url = "CategoryData/ListCategories/";
            response = client.GetAsync(url).Result;
            IEnumerable<CategoryDto> CategoryOptions = response.Content.ReadAsAsync<List<CategoryDto>>().Result;

            ViewModel.CategoryOptions = CategoryOptions;

            return View(ViewModel);
        }

        // POST: Event/UpdateEvent/9
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Update(int id, Event newEvent, HttpPostedFileBase EventImage)
        {
            
                Debug.WriteLine("event info : ");
                Debug.WriteLine(newEvent.Title);
                Debug.WriteLine(newEvent.CategoryId);

                string convertedId = id.ToString();//super important!!!!
                string url = "EventData/UpdateEvent/" + convertedId;//In order to work , need a router like:"https://localhost:44317/api/"before string
                //serialize into JSON
                //Send the request to the API

                string jsonpayload = jss.Serialize(newEvent);

                Debug.WriteLine(jsonpayload);

                HttpContent content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";

                HttpResponseMessage response = client.PostAsync(url, content).Result;

                Debug.WriteLine("Content:"+content);

            //update request is successful, and we have image data
            if (response.IsSuccessStatusCode && EventImage != null)
            {
                Debug.WriteLine("Attempt to update img");
                // Read the response content as a string
                var responseData = response.Content.ReadAsAsync<Event>().Result;
                Debug.WriteLine("responseData" + responseData);
                // Deserialize the response to get the ID
                var eventId = responseData.EventId;
                Console.WriteLine($"Event updated successfully. ID: {eventId}");
                //Updating the animal picture as a separate request
                //Debug.WriteLine("Calling Add Image method.");
                //Send over image data for player
                url = "EventData/UploadEventImage/" + eventId;
                //Debug.WriteLine("Received  Picture "+Event.FileName);

                MultipartFormDataContent formData = new MultipartFormDataContent();
                HttpContent imagecontent = new StreamContent(EventImage.InputStream);
                formData.Add(imagecontent, "EventPic", EventImage.FileName);

                try
                {
                    response = client.PostAsync(url, formData).Result;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }


                return RedirectToAction("List");
            }
            else if (response.IsSuccessStatusCode)
            {
                //No image upload, but update still successful
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }


        /*  if (response.IsSuccessStatusCode) { 

          return RedirectToAction("Find/" + id);
          }
          else
           {
          return View();
           }
      }*/


        //GET : /Event/DeleteConfirm/{id}
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirm(int id)
        {
            string convertedId = id.ToString();//super important!!!!
            string url = "EventData/FindEvent/" + convertedId;//In order to work , need a router like:"https://localhost:44317/api/"before string
            HttpResponseMessage response = client.GetAsync(url).Result;

            EventDto SelectedEvent = response.Content.ReadAsAsync<EventDto>().Result;

            if (SelectedEvent == null)
            {
                // If the event is not found, return HttpNotFound
                return HttpNotFound();
            }

            return View(SelectedEvent);
        }




        //Post: Event/Delete/{id}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Debug.WriteLine("The Event is ");
            Debug.WriteLine(id);

                string convertedId = id.ToString();//super important!!!!
                string url = "EventData/DeleteEvent/" + convertedId;// In order to work, need a router like: "https://localhost:44317/api/"before string
            
                HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
                HttpResponseMessage response = client.PostAsync(url,content).Result;
              
                 if (response.IsSuccessStatusCode)
                 {
                     return RedirectToAction("List");
                 }
                 else
                 {

                     return RedirectToAction("Error");
                 }


        }
        


    }
}