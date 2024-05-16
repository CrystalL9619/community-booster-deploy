using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;//should add this to utilize the HttpClient
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using MyPassionProject.Models;//should add this to use CategoryDto 
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using MyPassionProject.Models.ViewModels;

namespace MyPassionProject.Controllers
{
    public class CategoryController : Controller
    {

        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static CategoryController()
        {
            client = new HttpClient();
            client.BaseAddress = new System.Uri("https://localhost:44317/api/");
        }


        // GET: Species/List
        // GET: Category/List
        public ActionResult List()//should be the same as viewbag.title 
        {
            //use HTTP client to access infomation
            //objective: communicate with our category data api to retrieve a list of Category
            //curl https://localhost:44317/api/CategoryData/ListCategories

           
            string url = "CategoryData/ListCategories";// In order to work , need a router like:" https://localhost:44317/api/"before string
            HttpResponseMessage response = client.GetAsync(url).Result;//According to your method, use GetAsync,PostAsync,or ReadAsAsync.
            List<CategoryDto> Categories = response.Content.ReadAsAsync<List<CategoryDto>>().Result;

            //Views/Category/List.cshtml
            return View(Categories);
        }

        //Get:Category/Find/2
        public ActionResult Find(int id)
        {
            FindCategory ViewModel = new FindCategory();

            //use HTTP client to access infomation
            //objective: communicate with our category data api to retrieve a specific  category by ID
            //curl https://localhost:44317/api/CategoryData/FindCategory/2

            string convertedId = id.ToString();//super important!!!!
            string url = "CategoryData/FindCategory/" + convertedId;//In order to work , need a router like:"https://localhost:44317/api/"before string
            HttpResponseMessage response = client.GetAsync(url).Result;

            CategoryDto SelectedCategory = response.Content.ReadAsAsync<CategoryDto>().Result;

            Debug.WriteLine("Category received : ");
            Debug.WriteLine(SelectedCategory.CategoryName);
        
            ViewModel.SelectedCategory = SelectedCategory;

            //showcase information about events related to this category
            //send a request to gather information about events related to a particular category ID
            url = "EventData/ListEventsForCategory/" + convertedId;
            response = client.GetAsync(url).Result;
            // Deserialize the response into a list of EventDto objects
            IEnumerable<EventDto> RelatedEvents = response.Content.ReadAsAsync<IEnumerable<EventDto>>().Result;
            ViewModel.RelatedEvents = RelatedEvents;



            return View(ViewModel);
        }

        public ActionResult Error()
        {

            return View();
        }


        // GET: Category/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(Category newCategory)
        {
            Debug.WriteLine("the json payload is :");

            //Debug.WriteLine(Category.CategoryName);
            //objective: add a new category into our system using the API


            string url = "CategoryData/AddCategory";//In order to work , need a router like:"https://localhost:44317/api/"before string

            string jsonpayload = jss.Serialize(newCategory);

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


        // GET: Category/Edit/2
        public ActionResult Edit(int id)
        {
            //grab the Category information

            //objective: communicate with our category data api to retrieve a specific category by ID

            string convertedId = id.ToString();//super important!!!!
            string url = "CategoryData/FindCategory/" + convertedId; //In order to work, need a router like: "https://localhost:44317/api/"before string
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            CategoryDto SelectedCategory = response.Content.ReadAsAsync<CategoryDto>().Result;

            return View(SelectedCategory);
        }

        // POST: Category/UpdateCategory/2
        [HttpPost]
        public ActionResult Update(int id, Category newCategory)
        {
            try
            {
                Debug.WriteLine("category info : ");
                Debug.WriteLine(newCategory.CategoryId);
                Debug.WriteLine(newCategory.CategoryName);

                string convertedId = id.ToString();//super important!!!!
                string url = "CategoryData/UpdateCategory/" + convertedId;//In order to work , need a router like:"https://localhost:44317/api/"before string
                //serialize into JSON
                //Send the request to the API

                JavaScriptSerializer jss = new JavaScriptSerializer();
                string jsonpayload = jss.Serialize(newCategory);

                Debug.WriteLine(jsonpayload);

                HttpContent content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";

                // POST: api/CategoryData/UpdateCategory/2
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

        //GET : /Category/DeleteConfirm/{id}
        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
       
            string convertedId = id.ToString();//super important!!!!
            string url = "CategoryData/FindCategory/" + convertedId;//In order to work , need a router like:"https://localhost:44317/api/"before string
            HttpResponseMessage response = client.GetAsync(url).Result;

            CategoryDto SelectedCategory = response.Content.ReadAsAsync<CategoryDto>().Result;

            if (SelectedCategory == null)
            {
                // If the event is not found, return HttpNotFound
                return HttpNotFound();
            }

            return View(SelectedCategory);
        }




        //Post: Category/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Debug.WriteLine("The Category is ");
            Debug.WriteLine(id);


                string convertedId = id.ToString();//super important!!!!
                string url = "CategoryData/DeleteCategory/" + convertedId;// In order to work, need a router like: "https://localhost:44317/api/"before string
           
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
}