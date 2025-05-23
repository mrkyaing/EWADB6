﻿namespace CloudHRMS.Domain.Models.ViewModels {
    public class PayrollProcessViewModel {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string EmployeeId { get; set; }
        public string DepartmentId { get; set; }
        //for dropdownload
        public IEnumerable<EmployeeViewModel> Employees { get; set; }
        public IEnumerable<DepartmentViewModel> Departments { get; set; }
    }
}
