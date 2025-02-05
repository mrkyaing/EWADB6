using Microsoft.AspNetCore.Mvc;

namespace WorkOutMVC.Controllers
{
    public class ConvertorController : Controller
    {
        //For Initlal loading UI page
        [HttpGet]
        public IActionResult ConvertorV1()
        {
            return View();
        }
        //When submit the Calculate process by the user.
        [HttpPost]
        //Binding approch in UI to server side 
        public IActionResult ConvertorV1(string fromCurrency,decimal inputAmount)
        {
            decimal result = 0;
            switch (fromCurrency)
            {
                case "usd":result = inputAmount * 1300; break;
                case "sdg": result = inputAmount * 1100; break;
                case "baht": result = inputAmount * 100; break;
            }
            ViewBag.CalculatedResult= result;
            return View();
        }

        //For Initlal loading UI page
        [HttpGet]
        public IActionResult ConvertorV2()
        {
            return View();
        }
        //When submit the Calculate process by the user.
        [HttpPost]
        //Binding approch in UI to server side 
        public IActionResult ConvertorV2(string fromCurrency, decimal inputAmount)
        {
            decimal result = 0;
            switch (fromCurrency)
            {
                case "usd": result = inputAmount * 1300; break;
                case "sdg": result = inputAmount * 1100; break;
                case "baht": result = inputAmount * 100; break;
            }
            ViewData["fromCurrency"] = fromCurrency;
            ViewData["inputAmount"] = inputAmount;
            ViewBag.CalculatedResult = result;
            return View();
        }
    }
}
