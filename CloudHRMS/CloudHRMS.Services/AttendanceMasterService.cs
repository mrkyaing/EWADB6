using CloudHRMS.Domain.Models.DataModels;
using CloudHRMS.Domain.Models.ViewModels;
using CloudHRMS.Domain.Utlitity;
using CloudHRMS.UnitOfWorks;
using System.Data;

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

                //get the daily atttendance accroding to assigned shift on that date with LINQ query
                var DailyAttendancesWithShiftAssignsData = (from da in _unitOfWork.DailyAttendanceRepository.GetAll(w => w.IsActive)
                                                            join sa in _unitOfWork.ShiftAssignRepository.GetAll(w => w.IsActive)
                                                            on da.EmployeeId equals sa.EmployeeId
                                                            where sa.EmployeeId == ui.EmployeeId &&
                                                            (ui.AttendanceDate >= sa.FromDate && sa.FromDate <= ui.ToDate)
                                                            select new {
                                                                dailyAttendance = da,
                                                                shiftAssign = sa
                                                            }).ToList();
                //or you can use Native Query (SQL)

                // check  he/she IsLate, IsEarlyOut, IsLeave
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
                        if (data.dailyAttendance.InTime > definedShift.LateAfter) {//10:00>09:15
                            attendanceMaster.IsLate = true;
                        }
                        else {
                            attendanceMaster.IsLate = false;
                        }
                        //checking out the late status
                        if (data.dailyAttendance.OutTime < definedShift.EarlyOutBefore) {// 17:44 < 17:45
                            attendanceMaster.IsEarlyOut = true;
                        }
                        else {
                            attendanceMaster.IsEarlyOut = false;
                        }
                        attendanceMasters.Add(attendanceMaster);
                    }//end of the deifned shift not null

                }
                //insert the attendance data to the attendance master table from this raw data ( DailyAttendance )
                _unitOfWork.AttendanceMasterRepository.Create(attendanceMasters);
                _unitOfWork.Commit();
            }
            catch (Exception ex) {
                _unitOfWork.Rollback();
            }
        }

        public void Delete(DateTime fromDate, DateTime toDate, string departmentId, string employeeId) {
            try {
                IEnumerable<AttendanceMasterEntity> collectAttendanceMasters = null;
                // user select the department from UI
                if (departmentId is not null) {
                    collectAttendanceMasters = _unitOfWork.AttendanceMasterRepository.GetAll(w => (w.AttendanceDate.Date >= fromDate.Date && w.AttendanceDate.Date <= toDate.Date) && w.DepartmentId == departmentId).ToList();
                }
                // user select the employee from UI
                else if (employeeId is not null) {
                    collectAttendanceMasters = _unitOfWork.AttendanceMasterRepository.GetAll(w => (w.AttendanceDate.Date >= fromDate.Date && w.AttendanceDate.Date <= toDate.Date) && w.EmployeeId == employeeId).ToList();
                }
                // user select both 
                else {
                    collectAttendanceMasters = _unitOfWork.AttendanceMasterRepository.GetAll(w => (w.AttendanceDate.Date >= fromDate.Date && w.AttendanceDate.Date <= toDate.Date) && w.DepartmentId == departmentId && w.EmployeeId == employeeId).ToList();
                }
                foreach (var attendance in collectAttendanceMasters) {
                    // delete the actual delete process : mean remove the recrod from the database
                    _unitOfWork.AttendanceMasterRepository.Delete(attendance, true);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception ex) {
                _unitOfWork.Rollback();
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
