using System.ComponentModel.DataAnnotations;
namespace CloudHRMS.Models.DataModels {
    public abstract class BaseEntity {
        [Key] //Model Attribute in MVC
        [MaxLength(36)]
        public required string Id { get; set; }//for Pramary Key in DB
        public required DateTime CreatedAt { get; set; }//for audit prupose
        public required string CreatedBy { get; set; }///for audit prupose
        public DateTime? UpdatedAt { get; set; }///for audit prupose
        public string? UpdatedBy { get; set; }///for audit prupose
        public required string Ip { get; set; }//for audit prupose
        public required bool IsActive { get; set; }//Soft Delete
    }
}
