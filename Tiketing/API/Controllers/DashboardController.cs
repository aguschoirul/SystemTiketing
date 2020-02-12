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

    public class DashboardController : ApiController
    {
        ApplicationDbContext myContext = new ApplicationDbContext();

        public IQueryable<TicketVM> GetTicket()
        {
            return myContext.Ticket;
        }

        [ResponseType(typeof(TicketVM))]
        public IHttpActionResult GetTicket(int id)
        {
            TicketVM ticket = myContext.Ticket.Find(id);
            return Ok(ticket);
        }

        [ResponseType(typeof(TicketVM))]
        public IHttpActionResult Post(TicketVM ticket)
        {
            myContext.Ticket.Add(ticket);
            myContext.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = ticket.id }, ticket);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(TicketVM ticket, int id)
        {
            var put = myContext.Ticket.Find(id);
            put.subject = ticket.subject;
            myContext.Entry(put).State = EntityState.Modified;
            myContext.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(TicketVM ticket, int id)
        {
            var del = myContext.Ticket.Find(id);
            myContext.Ticket.Remove(del);
            myContext.SaveChanges();

            return Ok(del);
        }
    }
}
