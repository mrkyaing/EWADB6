using Microsoft.AspNetCore.Mvc;

namespace SampleMVC.Controllers {
    public class HomeController : Controller {

        // Index,SayHi,TellMeNow are action methods
        //port:/home/index
        public IActionResult Index() {
            string message;
            int hour = DateTime.Now.Hour;
            message = hour < 12 ? "Good Morning" : "Good Evening";
            ViewBag.Greeting = message;
            TempData["msg"] = "Hi, I am from TempData";
            return View();
        }
        //port:/home/sayhi
        public string SayHi() => "Hi,I am from Home controller ";

        //port:/home/tellmenow
        public string TellMeNow() => DateTime.Now.ToString();

        public JsonResult WhoAmI() {
            var msg = TempData["msg"];
            return Json(new { Name = "MG", Message = msg });
        }
        //port:/home/getresult >>2
        public int GetResult() => 1 + 1;

        //port:/home/sum?n1=10.2&n2=20.6
        public decimal Sum(decimal n1, decimal n2) => n1 + n2;

        [HttpGet]
        public string TellMeFullName(string fristName, string lastName) {
            string fullName = $"{fristName} {lastName}";
            var msg = TempData["msg"];
            return string.IsNullOrWhiteSpace(fullName) ? "Unknow User!!" + msg : $"Hello,User :{fullName}";
        }

        [NonAction]
        [ActionName("downloadclick")]
        public FileResult DownloadFile() {
            string fileName = "code.png";
            string filePath = Path.Combine("wwwroot/images", fileName);//wwwroot/images/code.jpg
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/octet-stream", fileName);
        }

        [HttpGet]
        public int GetSum(int n1, int n2) {
            int sum = n1 + n2;
            return sum;
        }

        [HttpPost]
        public int PostSum(int n1, int n2) {
            int sum = n1 + n2;
            return sum;
        }
    }
}
