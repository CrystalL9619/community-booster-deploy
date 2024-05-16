using MyPassionProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Reflection;

namespace MyPassionProject.Controllers
{
    public class CategoryDataController : ApiController
    {

        //Utlizing the database connection 
        private ApplicationDbContext db = new ApplicationDbContext();
        private object modelBuilder;

        //ListCategories
        //GET:"api/CategoryData/ListCategories
        [HttpGet]
        [ResponseType(typeof(CategoryDto))]
        [Route("api/CategoryData/ListCategories")]
        public List<CategoryDto> ListCategories()
        {
            List<Category> Categories = db.Categories.ToList();
            List<CategoryDto> CategoryDtos = new List<CategoryDto>();
            Categories.ForEach(c => CategoryDtos.Add(new CategoryDto()
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            }));
            return CategoryDtos;
        }

        //FindCategory
        // GET: api/CategoryData/FindCategory/2
        [ResponseType(typeof(CategoryDto))]
        [HttpGet]
        public IHttpActionResult FindCategory(int id)
        {
            Category Category = db.Categories.Find(id);
            CategoryDto CategoryDto = new CategoryDto()
            {
                CategoryId = Category.CategoryId,

                CategoryName = Category.CategoryName
            };
            if (Category == null)
            {
                return NotFound();
            }

            return Ok(CategoryDto);
        }

        //AddCategory
        // POST: api/CategoryData/AddCategory
        [ResponseType(typeof(Category))]
        [HttpPost]
        public IHttpActionResult AddCategory(Category newCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            db.Categories.Add(newCategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = newCategory.CategoryId }, newCategory);
        }


        // UpdateCategory
        // POST: api/CategoryData/UpdateCategory/1
        
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateCategory(int id, Category updatedCategory)
        {
            Debug.WriteLine("I have reached the update event method!");

            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            Category existingCategory = db.Categories.Find(id);

            if (existingCategory == null)
            {
                Debug.WriteLine("Category not found");
                return NotFound();
            }

            
            existingCategory.CategoryName = updatedCategory.CategoryName ?? existingCategory.CategoryName;

            try
            {
                db.SaveChanges();
                Debug.WriteLine("Category updated successfully");
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    Debug.WriteLine("Category not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        //DeleteCategory
        // POST: api/CategoryData/DeleteCategory/1
        [ResponseType(typeof(Category))]
        [HttpPost]
        public IHttpActionResult DeleteCategory(int id)
        {
            Category existingCategory = db.Categories.Find(id);
            if (existingCategory == null)
            {
                return NotFound();
            }

            db.Categories.Remove(existingCategory);
            db.SaveChanges();

            return Ok();
        }

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(c => c.CategoryId == id) > 0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



























    }
}
