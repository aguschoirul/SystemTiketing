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
    public class DivisionController : Controller
    {
        // GET: Division
        readonly HttpClient Client = new HttpClient();

        public DivisionController()
        {
            Client.BaseAddress = new Uri("http://localhost:56439/API/");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("ApplicationException/Json"));
        }
        // GET: Division
        public ActionResult Index()
        {
            return View(List());
        }

        public JsonResult List()
        {
            IEnumerable<DivisionVM> division = null;
            var responTask = Client.GetAsync("Division");
            responTask.Wait();

            var result = responTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<DivisionVM>>();
                readTask.Wait();
                division = readTask.Result;
            }
            else
            {
                division = Enumerable.Empty<DivisionVM>();
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return Json(new { data = division }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Create(DivisionVM division)
        {
            var affectedRow = Client.PostAsJsonAsync("Division", division).ToString();
            return Json(new { data = affectedRow }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(DivisionVM division, int id)
        {
            var affectedRow = Client.DeleteAsync("Division/" + id).ToString();
            return Json(new { data = affectedRow }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Detail(int id)
        {
            DivisionVM division = null;
            var responseTask = Client.GetAsync("Division/" + id.ToString());
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<DivisionVM>();
                readTask.Wait();
                division = readTask.Result;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "404 Not Found");
            }
            return Json(new { data = division }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Edit(DivisionVM division, int id)
        {
            var affectedRow = Client.PutAsJsonAsync("Division/" + id, division).ToString();
            return Json(new { data = affectedRow }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult loadDivision()
        {
            IEnumerable<DivisionVM> division = null;
            var responTask = Client.GetAsync("Division");
            responTask.Wait();

            var result = responTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IEnumerable<DivisionVM>>();
                readTask.Wait();
                division = readTask.Result;
            }
            else
            {
                division = Enumerable.Empty<DivisionVM>();
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return Json(division, JsonRequestBehavior.AllowGet);
        }
    }
}