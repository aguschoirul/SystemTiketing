using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using API.ViewModel;

namespace SystemTicketing.Controllers
{
    public class EmployeeController : Controller
    {
        readonly HttpClient Client = new HttpClient();

        public EmployeeController()
        {
            Client.BaseAddress = new Uri("https://brmapi.azurewebsites.net/API/");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("ApplicationException/Json"));
        }

        // GET: Employee

        public JsonResult loadEmployee()
        {
            IEnumerable<EmployeeVM> employee = null;
            var responTask = Client.GetAsync("Employees");
            responTask.Wait();

            var result = responTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IEnumerable<EmployeeVM>>();
                readTask.Wait();
                employee = readTask.Result;
            }
            else
            {
                employee = Enumerable.Empty<EmployeeVM>();
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return Json(employee, JsonRequestBehavior.AllowGet);
        }
    }
}