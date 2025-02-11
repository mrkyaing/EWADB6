using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.DataModels {
    [Table("DailyAttendance")]
    public class DailyAttendanceEntity : BaseEntity {
        public required DateTime AttendanceDate { get; set; }
        public required TimeSpan InTime { get; set; }
        public required TimeSpan OutTime { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public required string EmployeeId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public required string DepartmentId { get; set; }

    }
}