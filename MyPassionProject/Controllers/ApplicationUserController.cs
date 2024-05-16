using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;//should add this to utilize the HttpClient
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using MyPassionProject.Models;//should add this to use AppUserDto 
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using MyPassionProject.Models.ViewModels;

namespace MyPassionProject.Controllers
{/*
    public class AppUuserController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static AppUserController()
        {
            client = new HttpClient();
            client.BaseAddress = new System.Uri("https://localhost:44317/api/");
        }


        // GET: AppUser/List
        public ActionResult List()//should be the same as viewbag.title 
        {
            //use HTTP client to access infomation
            //objective: communicate with our AppUser data api to retrieve a list of AppUser
            //curl https://localhost:44317/api/AppUserData/ListAppUsers

          
            string url = "AppUserData/ListAppUsers";// In order to work , need a router like:" https://localhost:44317/api/"before string
            HttpResponseMessage response = client.GetAsync(url).Result;//According to your method, use GetAsync,PostAsync,or ReadAsAsync.
            List<ApplicationUser> AppUsers = response.Content.ReadAsAsync<List<AppUserDto>>().Result;

            //Views/AppUser/List.cshtml
            return View(AppUsers);
        }

        //Get:AppUser/Find/2
        public ActionResult Find(int id)
        {
            FindAppUser ViewModel = new FindAppUser();
            //use HTTP client to access infomation
            //objective: communicate with our AppUser data api to retrieve a specific AppUser by ID
            //curl https://localhost:44317/api/AppUserData/FindAppUser/2

         
            string convertedId = id.ToString();//super important!!!!
            string url = "AppUserData/FindAppUser/" + convertedId;//In order to work , need a router like:"https://localhost:44317/api/"before string
            HttpResponseMessage response = client.GetAsync(url).Result;

            AppUserDto SelectedAppUser = response.Content.ReadAsAsync<AppUserDto>().Result;

            Debug.WriteLine("AppUser received : ");
            Debug.WriteLine(SelectedAppUser.UserName);

            ViewModel.SelectedAppUser = SelectedAppUser;

            //show all events under this user
            url = "EventData/ListEventsForAppUser/" + convertedId;
            response = client.GetAsync(url).Result;
            IEnumerable<EventDto> PaticipatingEvents = response.Content.ReadAsAsync<IEnumerable<EventDto>>().Result;

            ViewModel.PaticipatingEvents = PaticipatingEvents;

            return View(ViewModel);
        }


        //POST: AppUser/Associate/{EventId}/{UserId}
        

        
        public ActionResult Error()
        {

            return View();
        }


        // GET: AppUser/New
        public ActionResult New()
        {
            return View();
        }
        // POST: AppUser/Create
        [HttpPost]
        public ActionResult Create(AppUser newAppUser)
        {
            Debug.WriteLine("the json payload is :");

            //Debug.WriteLine(AppUser.AppUserName);
            //objective: add a new AppUser into our system using the API


            string url = "AppUserData/AddAppUser";//In order to work , need a router like:"https://localhost:44317/api/"before string

            string jsonpayload = jss.Serialize(newAppUser);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

          
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: AppUser/Edit/2
        public ActionResult Edit(int id)
        {
            //grab the AppUser information

            //objective: communicate with our AppUser data api to retrieve a specific AppUser by ID

            string convertedId = id.ToString();//super important!!!!
            string url = "AppUserData/FindAppUser/" + convertedId; //In order to work, need a router like: "https://localhost:44317/api/"before string
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            AppUserDto SelectedAppUser = response.Content.ReadAsAsync<AppUserDto>().Result;

            return View(SelectedAppUser);
        }

        // POST: AppUser/UpdateAppUser/2
        [HttpPost]
        public ActionResult Update(int id, AppUser newAppUser)
        {
            try { 
                Debug.WriteLine("AppUser info : ");
                Debug.WriteLine(newAppUser.UserId);
                Debug.WriteLine(newAppUser.UserName);

                string convertedId = id.ToString();//super important!!!!
                string url = "AppUserData/UpdateAppUser/" + convertedId;//In order to work , need a router like:"https://localhost:44317/api/"before string
                //serialize into JSON
                //Send the request to the API

                string jsonpayload = jss.Serialize(newAppUser);

                Debug.WriteLine(jsonpayload);

                HttpContent content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";

                // POST: api/AppUserData/UpdateAppUser/2
                //Header : Content-Type: application/json

                HttpResponseMessage response = client.PostAsync(url, content).Result;

                Debug.WriteLine(response);


                return RedirectToAction("Find/" + id);
            }
            catch
            {
                return View();
            }
        }

        //GET : /AppUser/DeleteConfirm/{id}
        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
      
            string convertedId = id.ToString();//super important!!!!
            string url = "AppUserData/FindAppUser/" + convertedId;//In order to work , need a router like:"https://localhost:44317/api/"before string
            HttpResponseMessage response = client.GetAsync(url).Result;

            AppUserDto SelectedAppUser = response.Content.ReadAsAsync<AppUserDto>().Result;

            if (SelectedAppUser == null)
            {
                // If the event is not found, return HttpNotFound
                return HttpNotFound();
            }

            return View(SelectedAppUser);
        }




        //Post: AppUser/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Debug.WriteLine("The AppUser is ");
            Debug.WriteLine(id);

                string convertedId = id.ToString();//super important!!!!
                string url = "AppUserData/DeleteAppUser/" + convertedId;// In order to work, need a router like: "https://localhost:44317/api/"before string

                HttpContent content = new StringContent("");
                HttpResponseMessage response = client.PostAsync(url, content).Result;
         

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

    */


    }
