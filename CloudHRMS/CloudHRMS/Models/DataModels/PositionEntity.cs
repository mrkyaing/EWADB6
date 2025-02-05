using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.DataModels {
    [Table("Position")]
    public class PositionEntity : BaseEntity {
        public required string Code { get; set; }
        public required string Description { get; set; }
        public required int Level { get; set; }
    }
}