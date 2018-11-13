using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Epione.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Epione.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
           "http://localhost:8089/Epione-web/chat/all/1");
            var client = new HttpClient();
            
            var response = await client.SendAsync(request);

            var byteArray = response.Content.ReadAsByteArrayAsync().Result;
            var result = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            var jsonObjects = JsonConvert.DeserializeObject<JArray>(result);
            Debug.WriteLine(jsonObjects);
            var list = jsonObjects.Value<JArray>().ToObject<List<discussion>>();
            Debug.WriteLine(list.First().ToString());
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}