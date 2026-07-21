using Microsoft.AspNetCore.Mvc;

namespace ResponseAiServer.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
