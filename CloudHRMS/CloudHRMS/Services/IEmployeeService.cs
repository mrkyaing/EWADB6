using CloudHRMS.Models.ReportModels;

namespace CloudHRMS.Services {
    public interface IEmployeeService {
        IEnumerable<EmployeeDetailModel> DetailBy(string fromCode, string toCode);
    }
}
