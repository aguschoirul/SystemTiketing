using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SystemTicketing.Controllers
{
    public class DashboardController : Controller
    {
        readonly HttpClient Client = new HttpClient();

        public DashboardController()
        {
            Client.BaseAddress = new Uri("http://localhost:56439/API/");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("ApplicationException/Json"));
        }

        // GET: Dashboard
        public ActionResult Index()
        {
            return View(List());
        }

        public JsonResult List()
        {
            IEnumerable<TicketVM> ticket = null;
            var responTask = Client.GetAsync("Ticket");
            responTask.Wait();

            var result = responTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<TicketVM>>();
                readTask.Wait();
                ticket = readTask.Result;
            }
            else
            {
                ticket = Enumerable.Empty<TicketVM>();
                ModelState.AddModelError(string.Empty, "Server Eror");
            }
            return Json(new { data = ticket }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Create(TicketVM ticket)
        {
            var affectedRow = Client.PostAsJsonAsync("Ticket", ticket).ToString();
            return Json(new { data = affectedRow }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(TicketVM ticket, int id)
        {
            var affectedRow = Client.DeleteAsync("Ticket/" + id).ToString();
            return Json(new { data = affectedRow }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Detail(int id)
        {
            TicketVM ticket = null;
            var responseTask = Client.GetAsync("Ticket/" + id.ToString());
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<TicketVM>();
                readTask.Wait();
                ticket = readTask.Result;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "404 Not Found");
            }
            return Json(new { data = ticket }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Edit(TicketVM ticket, int id)
        {
            var affectedRow = Client.PutAsJsonAsync("Ticket/" + id, ticket).ToString();
            return Json(new { data = affectedRow }, JsonRequestBehavior.AllowGet);
        }
    }
}