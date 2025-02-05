using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace MVCPractice.Controllers
{
    public class HomeController : Controller
    {

      
        //port://home/index
        [HttpGet]
        public IActionResult Index()
        {
            int n1 = 1;
            int n2 = 3;
            ViewBag.Result=n1+n2;
            GetCalculate(n1);
            TempData["test"] = "testing";
            return View();
        }

        public IActionResult Hi()
        {
            ViewData["Msg"] = "Hi";
            return View();
        }


        public IActionResult About()
        {
            var testing = TempData["test"];
            return View();
        }

        private int GetCalculate(int n)
        {
            n += 200;
            n /= 3;
            return n;
        }

        public IActionResult Contact()
        {
            var testing = TempData["test"];//can't access for TempData in here 
            return View();
        }
        //port://home/friends?closefriend=MGMG&mutalFriend=SUSU
        [HttpGet]
        public IActionResult Friends(string closeFriend,string mutalFriend)
        {
            ViewBag.Friends = $"My friends are {closeFriend} & {mutalFriend}";
            return View();
        }
        [NonAction]
        [HttpGet]
        //port://home/downlaodImage
        public FileResult DownloadImage()
        {
            string fileName = "aws.png";//wwwroot/MyFiles/aws.png;
            string filePath = Path.Combine("wwwroot/MyFiles", fileName);
            byte[] fileContents = System.IO.File.ReadAllBytes(filePath);//Stream
            return File(fileContents, "text/png", fileName);
        }

        [NonAction]
        public string SayHi() => "Hi";

        //Query String data passing 
        //URL pattern 
        //port://home/sum?n1=10&n2=10
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult Sum(int n1, int n2)
        {
            ViewBag.SumResult = n1 + n2;
            return View();
        }

        [HttpPost]
       public string  Register(string Name,string Email)
        {
            return $"Hello,You are registered with {Name} and {Email}";//Literal string message to the string .
        }
    }
}
