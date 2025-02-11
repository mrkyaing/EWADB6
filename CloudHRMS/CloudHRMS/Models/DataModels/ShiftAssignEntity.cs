using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.DataModels {
    [Table("ShiftAssign")]
    public class ShiftAssignEntity : BaseEntity {
        [ForeignKey(nameof(EmployeeId))]
        public required string EmployeeId { get; set; }
        [ForeignKey(nameof(ShiftId))]
        public required string ShiftId { get; set; }
        public required DateTime FromDate { get; set; }
        public required DateTime ToDate { get; set; }
    }
}