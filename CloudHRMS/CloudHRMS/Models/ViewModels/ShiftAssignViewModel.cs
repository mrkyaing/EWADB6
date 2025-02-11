namespace CloudHRMS.Models.ViewModels {
    public class ShiftAssignViewModel {
        public string Id { get; set; }
        public string EmployeeId { get; set; }

        public string ShiftId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string EmployeeName { get; set; }
        public string ShiftName { get; set; }
        public IList<EmployeeViewModel> Employees { get; set; }
        public IList<ShiftViewModel> Shifts { get; set; }
    }
}
