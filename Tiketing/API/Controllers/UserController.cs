using API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace API.Controllers
{
    public class UserController : ApiController
    {
        ApplicationDbContext myContext = new ApplicationDbContext();

        public IQueryable<UserVM> GetUser()
        {
            return myContext.User;
        }

        [ResponseType(typeof(UserVM))]
        public IHttpActionResult GetUser(int id)
        {
            UserVM user = myContext.User.Find(id);
            return Ok(user);
        }

        [ResponseType(typeof(UserVM))]
        public IHttpActionResult Post(UserVM user)
        {
            myContext.User.Add(user);
            myContext.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = user.id }, user);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(UserVM user, int id)
        {
            var put = myContext.User.Find(id);
            put.email = user.email;
            put.password = user.password;
            myContext.Entry(put).State = EntityState.Modified;
            myContext.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(RoleVM role, int id)
        {
            var del = myContext.Role.Find(id);
            myContext.Role.Remove(del);
            myContext.SaveChanges();

            return Ok(del);
        }
    }
}
