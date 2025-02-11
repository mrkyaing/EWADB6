using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.DataModels {
    [Table("AttendancePolicy")]
    public class AttendancePolicyEntity : BaseEntity {
        public required string Name { get; set; }
        public required int NumberOfLateTime { get; set; }
        public required int NumberOfEarlyOutTime { get; set; }
        public required decimal DeductionInAmount { get; set; }
        public required int DeductionInDay { get; set; }
    }
}