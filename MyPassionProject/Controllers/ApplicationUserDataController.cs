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
    public class ApplicationUserDataController : ApiController
    {
        /*
        //Utlizing the database connection 
        private ApplicationDbContext db = new ApplicationDbContext();
        private object modelBuilder;

        //ListAppUsers
        //GET:"api/AppUserData/ListAppUsers
        [HttpGet]
        [ResponseType(typeof(AppUserDto))]
        [Route("api/AppUserData/ListAppUsers")]
        public List<AppUserDto> ListAppUsers()
        {
            List<AppUser> AppUsers = db.AppUsers.ToList();
            List<AppUserDto> AppUserDtos = new List<AppUserDto>();
            AppUsers.ForEach(a => AppUserDtos.Add(new AppUserDto()
            {
                UserId = a.UserId,
                UserName = a.UserName,
                Password = a.Password,
                Email = a.Email,
                PhoneNumber = a.PhoneNumber,


            }));
            return AppUserDtos;
        }

        //ListAppUsersForEvent
        //GET:api/AppUserData/ListAppUsersForEvent/1
        [HttpGet]
        [ResponseType(typeof(AppUserDto))]
        public IHttpActionResult ListAppUsersForEvent(int id)
        {
            List<AppUser> AppUsers = db.AppUsers.Where(
                a => a.Events.Any(
                    e => e.EventId == id)
                ).ToList();
            List<AppUserDto> AppUserDtos = new List<AppUserDto>();
            AppUsers.ForEach(a => AppUserDtos.Add(new AppUserDto()
            { 
                 UserId = a.UserId,
                UserName = a.UserName,

            }));

            return Ok(AppUserDtos);
        }

        //ListAppUserNotForEvent
        //api/AppUserData/ListAppUserNotForEvent/1
        [HttpGet]
        [ResponseType(typeof(AppUserDto))]
        public IHttpActionResult ListAppUserNotForEvent(int id)
        {
            List<AppUser> AppUsers = db.AppUsers.Where(
                a => !a.Events.Any(
                    e => e.EventId == id)
                ).ToList();
            List<AppUserDto> AppUserDtos = new List<AppUserDto>();

            AppUsers.ForEach(a => AppUserDtos.Add(new AppUserDto()
            {
                UserId = a.UserId,
                UserName = a.UserName
              
            }));

            return Ok(AppUserDtos);
        }


        //FindAppUser
        // GET: api/AppUserData/FindAppUser/2
        [ResponseType(typeof(AppUserDto))]
        [HttpGet]
        public IHttpActionResult FindAppUser(int id)
        {
            AppUser AppUser = db.AppUsers.Find(id);
            AppUserDto AppUserDto = new AppUserDto()
            {
                UserId = AppUser.UserId,

                UserName = AppUser.UserName,
                Password = AppUser.Password,
                Email = AppUser.Email,
                PhoneNumber = AppUser.PhoneNumber
            };
            if (AppUser == null)
            {
                return NotFound();
            }

            return Ok(AppUserDto);
        }

        //AddAppUser
        // POST: api/AppUserData/AddAppUser
        [ResponseType(typeof(AppUser))]
        [HttpPost]
        public IHttpActionResult AddAppUser(AppUser newAppUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            db.AppUsers.Add(newAppUser);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = newAppUser.UserId }, newAppUser);
        }


        // UpdateAppUser
        // POST: api/AppUserData/UpdateAppUser/1

        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateAppUser(int id, AppUser updatedAppUser)
        {
            Debug.WriteLine("I have reached the update event method!");

            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            AppUser existingAppUser = db.AppUsers.Find(id);

            if (existingAppUser == null)
            {
                Debug.WriteLine("AppUser not found");
                return NotFound();
            }


            existingAppUser.UserName = updatedAppUser.UserName ?? existingAppUser.UserName;
            existingAppUser.Password = updatedAppUser.Password ?? existingAppUser.Password;
            existingAppUser.Email = updatedAppUser.Email ?? existingAppUser.Email;
            existingAppUser.PhoneNumber = updatedAppUser.PhoneNumber ?? existingAppUser.PhoneNumber;

            try
            {
                db.SaveChanges();
                Debug.WriteLine("AppUser updated successfully");
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppUserExists(id))
                {
                    Debug.WriteLine("AppUser not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }


        //DeleteAppUser
        // POST: api/AppUserData/DeleteAppUser/1
        [ResponseType(typeof(AppUser))]
        [HttpPost]
        public IHttpActionResult DeleteAppUser(int id)
        {
            AppUser existingAppUser = db.AppUsers.Find(id);
            Debug.WriteLine($"{existingAppUser.UserName}"); 
            if (existingAppUser == null)
            {
                return NotFound();
            }

            db.AppUsers.Remove(existingAppUser);
            db.SaveChanges();

            return Ok();
        }

        private bool AppUserExists(int id)
        {
            return db.AppUsers.Count(a => a.UserId == id) > 0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }





        */
    }

}
