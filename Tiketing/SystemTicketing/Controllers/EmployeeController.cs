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
    public class EmployeeController : Controller
    {
        readonly HttpClient Client = new HttpClient();

        public EmployeeController()
        {
            Client.BaseAddress = new Uri("http://localhost:56439/API/");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("ApplicationException/Json"));
        }

        // GET: Employee
        public ActionResult Index()
        {
            return View(List());
        }

        public JsonResult List()
        {
            IEnumerable<EmployeeVM> Employee = null;
            var responTask = Client.GetAsync("Employee");
            responTask.Wait();

            var result = responTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<EmployeeVM>>();
                readTask.Wait();
                Employee = readTask.Result;
            }
            else
            {
                Employee = Enumerable.Empty<EmployeeVM>();
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return Json(Employee);
        }
    }
}