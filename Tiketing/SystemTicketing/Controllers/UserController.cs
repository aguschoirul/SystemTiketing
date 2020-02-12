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
    public class UserController : Controller
    {

        readonly HttpClient Client = new HttpClient();

        public UserController()
        {
            Client.BaseAddress = new Uri("http://localhost:56439/API/");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("ApplicationException/Json"));
        }

        // GET: User
        public ActionResult Index()
        {
            return View(List());
        }

        public JsonResult List()
        {
            IEnumerable<UserVM> User = null;
            var responTask = Client.GetAsync("Employees");
            responTask.Wait();
            var result = responTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<UserVM>>();
                readTask.Wait();
                User = readTask.Result;
            }
            else
            {
                User = Enumerable.Empty<UserVM>();
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return Json(new { data = User }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Create(UserVM user)
        {
            var affectedRow = Client.PostAsJsonAsync("User", user).ToString();
            return Json(new { data = affectedRow }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(UserVM user, int id)
        {
            var affectedRow = Client.DeleteAsync("User/" + id).ToString();
            return Json(new { data = affectedRow }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Detail(int id)
        {
            UserVM user = null;
            var responseTask = Client.GetAsync("User/" + id.ToString());
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<UserVM>();
                readTask.Wait();
                user = readTask.Result;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "404 Not Found");
            }
            return Json(new { data = user }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Edit(UserVM user, int id)
        {
            var affectedRow = Client.PutAsJsonAsync("User/" + id, user).ToString();
            return Json(new { data = affectedRow }, JsonRequestBehavior.AllowGet);
        }

    }
}