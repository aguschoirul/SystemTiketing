using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace SystemTicketing.Controllers
{
    public class RolesController : Controller
    {
        readonly HttpClient Client = new HttpClient();

        public RolesController()
        {
            Client.BaseAddress = new Uri("http://localhost:56439/API/");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("ApplicationException/Json"));
        }
        // GET: Roles
        public ActionResult Index()
        {
            return View(List());
        }

        public JsonResult List()
        {
            IEnumerable<RoleVM> Role = null;
            var responTask = Client.GetAsync("Roles");
            responTask.Wait();

            var result = responTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<RoleVM>>();
                readTask.Wait();
                Role = readTask.Result;
            }
            else
            {
                Role = Enumerable.Empty<RoleVM>();
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return Json(Role);
        }

    }
}