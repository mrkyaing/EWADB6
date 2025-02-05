namespace OOPDay5
{
    public class PayrollService : IPayrollService
    {
        public decimal CalcualteNatPay(decimal baseSalary, decimal attendanceDays, int workingDays, decimal benefit, decimal detuction)
        {
           decimal dailySalary = baseSalary / workingDays;
           decimal netPay =(dailySalary * attendanceDays)+ benefit - detuction;
           return netPay;
        }
    }
}