namespace CloudHRMS.Domain.Models.ViewModels {
    public class DashboardViewModel {
        public IEnumerable<EmployeeViewModel> NewEmployeesOfCurrentMonth { get; set; }
    }
}