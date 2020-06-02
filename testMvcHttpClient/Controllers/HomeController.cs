using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace testMvcHttpClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ActionResult> Index()
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var result = await httpClient.GetStringAsync(new Uri("https://blog.cashwu.com"));

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}