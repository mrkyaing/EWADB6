namespace CloudHRMS.Models.ViewModels {
    public class PositionViewModel {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }

        public override string ToString() {
            return $"code:{Code},description:{Description},level:{Level}";
        }
    }
}