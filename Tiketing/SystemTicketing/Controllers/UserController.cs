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
    public class UserController : Controller
    {

        readonly HttpClient Client = new HttpClient();

        public UserController()
        {
            Client.BaseAddress = new Uri("https://brmapi.azurewebsites.net/API/");
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
            return Json(User);
        }

    }
}