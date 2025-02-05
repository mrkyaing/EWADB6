namespace CloudHRMS.Models.ViewModels {
    public class EmployeeViewModel {
        public string Id { get; set; }//for delete and update porpose.
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public char Gender { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOE { get; set; }
        public DateTime? DOR { get; set; }
        public string Address { get; set; }
        public decimal BasicSalary { get; set; }
        public string Phone { get; set; }

        //forgein key in here.
        public string DepartmentId { get; set; } // for update /edit
        public string DepartmentInfo { get; set; } //for show the values 

        public string PositionId { get; set; }// for update/Edit 
        public string PositionInfo { get; set; } //for show the values 

        //for select box controls data bind
        public IList<PositionViewModel> PositionViewModels { get; set; }
        public IList<DepartmentViewModel> DepartmentViewModels { get; set; }
    }
}
