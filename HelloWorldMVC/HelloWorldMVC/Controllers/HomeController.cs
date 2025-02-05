using Microsoft.AspNetCore.Mvc;
namespace HelloWorldMVC.Controllers
{
    public class HomeController :Controller
    {
        //port://{controller}/sayhi
        //port://home/sayhi
        public string SayHi()
        {
            return "Hello,Nice to see you";
        }
        //port://home/index
        public ViewResult Index()
        {
            return View();
        }

         public ViewResult TellMeNow()
        {
            int hour=DateTime.Now.Hour;
            string message = hour < 12 ? "Good Morning" : "Good afternoon";//ternary operator style
            ViewBag.Msg= message;
            return View();
        }
    }
}
