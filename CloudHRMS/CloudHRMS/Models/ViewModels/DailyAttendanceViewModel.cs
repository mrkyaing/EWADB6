namespace CloudHRMS.Models.ViewModels {
    public class DailyAttendanceViewModel {
        public string Id { get; set; }
        public DateTime AttendanceDate { get; set; }
        public TimeSpan InTime { get; set; }
        public TimeSpan OutTime { get; set; }
        public string EmployeeId { get; set; }
        public string DepartmentId { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public IList<EmployeeViewModel> Employees { get; set; }
        public IList<DepartmentViewModel> Departments { get; set; }
    }
}