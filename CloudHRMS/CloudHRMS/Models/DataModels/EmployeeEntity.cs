using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.DataModels {
    [Table("Employee")] //Model Attribute in MVC
    public class EmployeeEntity : BaseEntity {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        [MaxLength(1)] //Model Attribute in MVC
        public required char Gender { get; set; }
        public required DateTime DOB { get; set; }
        public required DateTime DOE { get; set; }
        public DateTime? DOR { get; set; }//NULL >> ALLOWED NULL 
        public required string Address { get; set; }
        [Precision(18, 2)] //Model Attribute in MVC >>>>>>>100000.23
        public required decimal BasicSalary { get; set; }
        public required string Phone { get; set; }

        //forgein key in here.
        [ForeignKey(nameof(DepartmentId))]//  [ForeignKey("DepartmentId")]
        public required string DepartmentId { get; set; }

        //forgein key in here.
        [ForeignKey(nameof(PositionId))]
        public required string PositionId { get; set; }
    }
}
