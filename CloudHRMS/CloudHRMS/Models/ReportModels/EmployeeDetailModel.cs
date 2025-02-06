namespace CloudHRMS.Models.ReportModels {
    public class EmployeeDetailModel {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public char Gender { get; set; }
        public string DOB { get; set; }
        public string DOE { get; set; }
        public string DOR { get; set; }
        public string Address { get; set; }
        public decimal BasicSalary { get; set; }
        public string Phone { get; set; }
        public string DepartmentInfo { get; set; } //for show the values 
        public string PositionInfo { get; set; } //for show the values 
    }
}
