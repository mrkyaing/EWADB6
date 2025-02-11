using CloudHRMS.Models.DataModels;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.UnitOfWorks;
using CloudHRMS.Utlitity;

namespace CloudHRMS.Services {
    public class AttendanceMasterService : IAttendanceMasterService {

        private readonly IUnitOfWork _unitOfWork;

        public AttendanceMasterService(IUnitOfWork unitOfWork) {
            this._unitOfWork = unitOfWork;
        }

        public void DayEndProcess(AttendanceMasterViewModel ui) {
            try {
                //Data exchange from view model to data model by using automapper
                List<AttendanceMasterEntity> attendanceMasters = new List<AttendanceMasterEntity>();
                var DailyAttendancesWithShiftAssignsData = (from da in _unitOfWork.DailyAttendanceRepository.GetAll(w => w.IsActive)
                                                            join sa in _unitOfWork.ShiftAssignRepository.GetAll(w => w.IsActive)
                                                            on da.EmployeeId equals sa.EmployeeId
                                                            where sa.EmployeeId == ui.EmployeeId &&
                                                            (ui.AttendanceDate >= sa.FromDate && sa.FromDate <= ui.ToDate)
                                                            select new {
                                                                dailyAttendance = da,
                                                                shiftAssign = sa
                                                            }).ToList();
                foreach (var data in DailyAttendancesWithShiftAssignsData) {
                    ShiftEntity definedShift = _unitOfWork.ShiftRepository.GetBy(s => s.Id == data.shiftAssign.ShiftId).SingleOrDefault();
                    if (definedShift is not null) {
                        AttendanceMasterEntity attendanceMaster = new AttendanceMasterEntity() {
                            Id = Guid.NewGuid().ToString(),
                            IsLeave = false,
                            InTime = data.dailyAttendance.InTime,
                            OutTime = data.dailyAttendance.OutTime,
                            EmployeeId = data.shiftAssign.EmployeeId,
                            ShiftId = definedShift.Id,
                            DepartmentId = data.dailyAttendance.DepartmentId,
                            AttendanceDate = data.dailyAttendance.AttendanceDate,
                            CreatedBy = "MG",
                            CreatedAt = DateTime.Now,
                            IsActive = true,
                            Ip = NetworkHelper.GetIpAddress()
                        };

                        //checking out the late status
                        if (data.dailyAttendance.InTime > definedShift.LateAfter) {
                            attendanceMaster.IsLate = true;
                        }
                        else {
                            attendanceMaster.IsLate = false;
                        }
                        //checking out the late status
                        if (data.dailyAttendance.OutTime < definedShift.EarlyOutBefore) {
                            attendanceMaster.IsEarlyOut = true;
                        }
                        else {
                            attendanceMaster.IsEarlyOut = false;
                        }
                        attendanceMasters.Add(attendanceMaster);
                    }//end of the deifned shift not null

                }
                _unitOfWork.AttendanceMasterRepository.Create(attendanceMasters);
                _unitOfWork.Commit();
            }
            catch (Exception ex) {
                _unitOfWork.Rollback();
            }
        }

        public void Delete(DateTime attendanceDate, string departmentId, string employeeId) {
            try {
                var collectAttendanceMasters = _unitOfWork.AttendanceMasterRepository.GetAll(w => w.AttendanceDate.Date == attendanceDate.Date && w.DepartmentId == departmentId && w.EmployeeId == employeeId).ToList();
                foreach (var attendance in collectAttendanceMasters) {
                    _unitOfWork.AttendanceMasterRepository.Delete(attendance, true);
                }
            }
            catch (Exception ex) {
            }
        }

        public IEnumerable<AttendanceMasterViewModel> GetAll() {
            //mapper.map<source,destination>(dataList);
            return (from attMaster in _unitOfWork.AttendanceMasterRepository.GetAll(w => w.IsActive)
                    join e in _unitOfWork.EmployeeRepository.GetAll(w => w.IsActive) on attMaster.EmployeeId equals e.Id
                    join d in _unitOfWork.DepartmentRepository.GetAll(w => w.IsActive) on attMaster.DepartmentId equals d.Id
                    join s in _unitOfWork.ShiftRepository.GetAll(w => w.IsActive) on attMaster.ShiftId equals s.Id
                    select new AttendanceMasterViewModel {
                        Id = attMaster.Id,
                        AttendanceDate = attMaster.AttendanceDate,
                        InTime = attMaster.InTime,
                        OutTime = attMaster.OutTime,
                        IsEarlyOut = attMaster.IsEarlyOut,
                        IsLate = attMaster.IsLate,
                        DepartmentId = d.Code + "/" + d.Description,
                        EmployeeId = e.Code + "/" + e.Name,
                        ShiftId = s.Name
                    }).AsEnumerable();
        }
    }
}
