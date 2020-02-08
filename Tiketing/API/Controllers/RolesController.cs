using API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Security;

namespace API.Controllers
{
    public class RolesController : ApiController
    {
        ApplicationDbContext myContext = new ApplicationDbContext();

        public IQueryable<RoleVM> GetRoles()
        {
            return myContext.Role;
        }

        [ResponseType(typeof(RoleVM))]
        public IHttpActionResult GetRoles(int id)
        {
            RoleVM role = myContext.Role.Find(id);
            return Ok(role);
        }

        [ResponseType(typeof(RoleVM))]
        public IHttpActionResult Post(RoleVM role)
        {
            myContext.Role.Add(role);
            myContext.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = role.id }, role);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(RoleVM role, int id)
        {
            var put = myContext.Role.Find(id);
            put.name = role.name;
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
