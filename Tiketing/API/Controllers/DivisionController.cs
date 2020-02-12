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
    public class DivisionController : ApiController
    {
        ApplicationDbContext myContext = new ApplicationDbContext();

        public IQueryable<DivisionVM> GetDivision()
        {
            return myContext.Division;
        }

        [ResponseType(typeof(DivisionVM))]
        public IHttpActionResult GetDivision(int id)
        {
            DivisionVM division = myContext.Division.Find(id);
            return Ok(division);
        }

        [ResponseType(typeof(DivisionVM))]
        public IHttpActionResult Post(DivisionVM division)
        {
            myContext.Division.Add(division);
            myContext.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = division.id }, division);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(DivisionVM division, int id)
        {
            var put = myContext.Division.Find(id);
            put.name = division.name;
            myContext.Entry(put).State = EntityState.Modified;
            myContext.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(DivisionVM division, int id)
        {
            var del = myContext.Division.Find(id);
            myContext.Division.Remove(del);
            myContext.SaveChanges();

            return Ok(del);
        }
    }
}
