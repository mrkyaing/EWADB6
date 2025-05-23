﻿namespace CloudHRMS.Domain.Models.ViewModels {
    public class AttendanceMasterViewModel {
        public string Id { get; set; }
        public DateTime AttendanceDate { get; set; }
        public DateTime ToDate { get; set; }
        public TimeSpan InTime { get; set; }
        public TimeSpan OutTime { get; set; }
        public string EmployeeId { get; set; }
        public string DepartmentId { get; set; }
        public string ShiftId { get; set; }
        public bool IsLate { get; set; }
        public bool IsEarlyOut { get; set; }
        public bool IsLeave { get; set; }
        public IEnumerable<ShiftViewModel> Shifts { get; set; }
        public IEnumerable<EmployeeViewModel> Employees { get; set; }
        public IEnumerable<DepartmentViewModel> Departments { get; set; }
    }
}
