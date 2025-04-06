using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Domain.Models.DataModels {
    [Table("Position")]
    public class PositionEntity : BaseEntity {
        public required string Code { get; set; }
        public required string Description { get; set; }
        public required int Level { get; set; }

        public override string ToString() {
            return $"code:{Code},description:{Description},level:{Level}";
        }
    }
}