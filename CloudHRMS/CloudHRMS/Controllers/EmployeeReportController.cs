using CloudHRMS.Models.ReportModels;
using CloudHRMS.Services;
using CloudHRMS.Utlitity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers {
    //PoC >> Proof of Concept 
    //e=mc^2
    [Authorize(Roles = "HR")]
    public class EmployeeReportController : Controller {
        private readonly IEmployeeService _employeeService;

        public EmployeeReportController(IEmployeeService employeeService) {
            this._employeeService = employeeService;
        }
        //employeeReport/Detail
        public IActionResult Detail() {
            //  var employeeDetailModel = new EmployeeDetailModel();
            //  employeeDetailModel.FromCode=_employeeService.get
            return View();
        }
        [HttpPost]
        //how to see dimension for your data  
        public IActionResult Detail(string fromCode, string toCode) {
            string fileName = $"EmployeeDetail-{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.xlsx";
            string fileContextType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            List<EmployeeDetailModel> reportData = _employeeService.DetailBy(fromCode, toCode).ToList();
            if (reportData.Count > 0) {
                var fileOutput = ReportHelper.ExportToExcel(reportData, fileName);
                ViewBag.Msg = "Employee detail report is successfully exported.";
                return File(fileOutput, fileContextType, fileName);
            }
            else {
                ViewBag.Msg = "There is no data to export employee detail report.";
            }
            return View();
        }

        public IActionResult Summary() {
            return View();
        }

    }
}
