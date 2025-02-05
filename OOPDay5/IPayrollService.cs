namespace OOPDay5
{
    public interface IPayrollService
    {
        decimal  CalcualteNatPay(decimal baseSalary, decimal attendanceDays,int workingDays,decimal benefit,decimal detuction);      
    }
}