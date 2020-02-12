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
    public class RoleController : Controller
    {
        readonly HttpClient Client = new HttpClient();

        public RoleController()
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
            IEnumerable<RoleVM> role = null;
            var responTask = Client.GetAsync("Role");
            responTask.Wait();

            var result = responTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<RoleVM>>();
                readTask.Wait();
                role = readTask.Result;
            }
            else
            {
                role = Enumerable.Empty<RoleVM>();
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return Json(new { data = role }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Create(RoleVM role)
        {
            var affectedRow = Client.PostAsJsonAsync("Role", role).ToString();
            return Json(new { data = affectedRow }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(RoleVM role, int id)
        {
            var affectedRow = Client.DeleteAsync("Role/" + id).ToString();
            return Json(new { data = affectedRow }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Detail(int id)
        {
            RoleVM role = null;
            var responseTask = Client.GetAsync("Role/" + id.ToString());
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<RoleVM>();
                readTask.Wait();
                role = readTask.Result;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "404 Not Found");
            }
            return Json(new { data = role }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Edit(RoleVM role, int id)
        {
            var affectedRow = Client.PutAsJsonAsync("Role/" + id, role).ToString();
            return Json(new { data = affectedRow }, JsonRequestBehavior.AllowGet);
        }
    }
}