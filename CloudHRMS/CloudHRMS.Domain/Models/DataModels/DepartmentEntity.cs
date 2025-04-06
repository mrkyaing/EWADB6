using System.ComponentModel.DataAnnotations.Schema;
namespace CloudHRMS.Domain.Models.DataModels {
    [Table("Department")]
    public class DepartmentEntity : BaseEntity {
        public required string Code { get; set; }
        public required string Description { get; set; }
        public required string ExtensionPhone { get; set; }
    }
}