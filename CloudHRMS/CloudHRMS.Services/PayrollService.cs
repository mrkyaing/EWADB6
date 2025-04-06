using CloudHRMS.Domain.Models.DataModels;
using CloudHRMS.Domain.Models.ViewModels;
using CloudHRMS.Domain.Services;
using CloudHRMS.Domain.Utlitity;
using CloudHRMS.UnitOfWorks;
namespace CloudHRMS.Services {
    public class PayrollService : IPayrollService {

        private readonly IUnitOfWork _unitOfWork;

        public PayrollService(IUnitOfWork unitOfWork) {
            this._unitOfWork = unitOfWork;
        }

        public void PayrollProcess(PayrollProcessViewModel ui) {

            try {
                List<AttendanceMasterCalculatedData> attendanceMasterCalculatedData = new List<AttendanceMasterCalculatedData>();
                if (ui.DepartmentId != null) {
                    //HR,01-03-2024 to 31-03-2024
                    List<AttendanceMasterEntity> attendances = _unitOfWork.AttendanceMasterRepository.GetAll(w => w.DepartmentId == ui.DepartmentId && (w.AttendanceDate <= ui.ToDate)).OrderBy(o => o.AttendanceDate).ToList();
                    List<AttendanceMasterEntity> distinctEmployees = attendances.DistinctBy(e => e.EmployeeId).ToList();
                    foreach (AttendanceMasterEntity distinctEmployee in distinctEmployees) {
                        AttendanceMasterCalculatedData calculatedData = new AttendanceMasterCalculatedData();
                        calculatedData.DepartmentId = distinctEmployee.DepartmentId;
                        calculatedData.EmployeeId = distinctEmployee.EmployeeId;
                        calculatedData.FromDate = ui.FromDate;
                        calculatedData.ToDate = ui.ToDate;
                        calculatedData.BasicPay = _unitOfWork.EmployeeRepository.GetBy(w => w.Id == distinctEmployee.EmployeeId).FirstOrDefault().BasicSalary;
                        calculatedData.LateCount = attendances.Where(w => w.EmployeeId == distinctEmployee.EmployeeId && w.IsLate && (w.AttendanceDate >= ui.FromDate && w.AttendanceDate <= ui.ToDate)).Count();
                        calculatedData.EarlyOutCount = attendances.Where(w => w.EmployeeId == distinctEmployee.EmployeeId && w.IsEarlyOut && (w.AttendanceDate >= ui.FromDate && w.AttendanceDate <= ui.ToDate)).Count();
                        calculatedData.LeaveCount = attendances.Where(w => w.EmployeeId == distinctEmployee.EmployeeId && w.IsLeave && (w.AttendanceDate >= ui.FromDate && w.AttendanceDate <= ui.ToDate)).Count();
                        calculatedData.AttendanceDays = attendances.Where(w => w.EmployeeId == distinctEmployee.EmployeeId && w.IsLeave == false && (w.AttendanceDate >= ui.FromDate && w.AttendanceDate <= ui.ToDate)).Count();
                        attendanceMasterCalculatedData.Add(calculatedData);
                    }
                    List<PayrollEntity> payrolls = CalculatePayroll(attendanceMasterCalculatedData);
                    _unitOfWork.PayrollRepository.Create(payrolls);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception ex) {
                _unitOfWork.Rollback();
            }
        }

        private List<PayrollEntity> CalculatePayroll(List<AttendanceMasterCalculatedData> attendanceMasterCalculatedData) {
            List<PayrollEntity> payrolls = new List<PayrollEntity>();
            decimal incomeTax = 2000, allowance = 30000, deduction = 10000;
            int workingDays = 30;
            foreach (AttendanceMasterCalculatedData calculatedData in attendanceMasterCalculatedData) {
                PayrollEntity payroll = new PayrollEntity() {
                    Id = Guid.NewGuid().ToString(),
                    FromDate = calculatedData.FromDate,
                    ToDate = calculatedData.ToDate,
                    EmployeeId = calculatedData.EmployeeId,
                    DepartmentId = calculatedData.DepartmentId,
                    IncomeTax = incomeTax,
                    PayPerDay = (calculatedData.BasicPay / workingDays),//500000/30 >> 16,666.66666666667
                    Allowance = allowance,
                    Deduction = deduction,
                    AttendanceDays = calculatedData.AttendanceDays,
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    Ip = NetworkHelper.GetIpAddress(),
                    IsActive = true
                };
                payroll.AttendanceDeduction = CalculateAttendanceDeductionByAttendancePolicy(calculatedData.EmployeeId, payroll.PayPerDay, calculatedData.LateCount, calculatedData.EarlyOutCount);
                payroll.GrossPay = ((payroll.PayPerDay) * calculatedData.AttendanceDays) + allowance - payroll.AttendanceDeduction - deduction;
                payroll.NetPay = payroll.GrossPay - payroll.IncomeTax;
                payrolls.Add(payroll);
            }
            return payrolls;
        }

        private decimal CalculateAttendanceDeductionByAttendancePolicy(string EmployeeId, decimal PayPerDay, int lateCount, int earlyOutCount) {
            decimal attendanceDeduction = 0;
            var attendancePolicy = (from ap in _unitOfWork.AttendancePolicyRepository.GetAll(w => w.IsActive)
                                    join s in _unitOfWork.ShiftRepository.GetAll(w => w.IsActive)
                                    on ap.Id equals s.AttendancePolicyId
                                    join sa in _unitOfWork.ShiftAssignRepository.GetAll(w => w.IsActive)
                                    on s.Id equals sa.ShiftId
                                    where sa.EmployeeId == EmployeeId
                                    select ap).FirstOrDefault();
            if (attendancePolicy != null) {
                if (lateCount > attendancePolicy.NumberOfLateTime || attendancePolicy?.NumberOfEarlyOutTime < earlyOutCount) {
                    attendanceDeduction = attendancePolicy?.DeductionInAmount ?? 0;
                }
                if (attendancePolicy?.DeductionInDay > 0) {
                    attendanceDeduction += (PayPerDay * attendancePolicy?.DeductionInDay) ?? 0;
                }
            }
            return attendanceDeduction;
        }
        public void Delete(DateTime fromDate, DateTime toDate, string departmentId, string employeeId) // 1-10 >> 
        {
            try {
                var collectPayrolls = _unitOfWork.PayrollRepository.GetAll(w => (w.FromDate.Date >= fromDate.Date && w.ToDate.Date <= toDate.Date) && w.DepartmentId == departmentId && w.EmployeeId == employeeId).ToList();
                foreach (var payroll in collectPayrolls) {
                    _unitOfWork.PayrollRepository.Delete(payroll, true);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception ex) {
                _unitOfWork.Rollback();
            }
        }

        public IEnumerable<PayrollViewModel> GetAll() {
            return (from p in _unitOfWork.PayrollRepository.GetAll(w => w.IsActive)
                    join e in _unitOfWork.EmployeeRepository.GetAll(w => w.IsActive)
                    on p.EmployeeId equals e.Id
                    join d in _unitOfWork.DepartmentRepository.GetAll(w => w.IsActive)
                    on e.DepartmentId equals d.Id
                    select new PayrollViewModel {
                        Id = p.Id,
                        FromDate = p.FromDate,
                        ToDate = p.ToDate,
                        EmployeeInfo = e.Code + "/" + e.Name,
                        BasicSalary = e.BasicSalary,
                        DepartmentInfo = d.Code + "/" + d.Description,
                        GrossPay = p.GrossPay,
                        NetPay = p.NetPay,
                        AttendanceDays = p.AttendanceDays,
                        AttendanceDeduction = p.AttendanceDeduction,
                        Allowance = p.Allowance,
                        Deduction = p.Deduction,
                        IncomeTax = p.IncomeTax,
                        PayPerDay = p.PayPerDay
                    }).ToList();
        }
    }
}
