using Microsoft.AspNetCore.Mvc;

namespace SampleMVC.Controllers {
    public class AboutController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
