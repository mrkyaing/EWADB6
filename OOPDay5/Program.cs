using OOPDay5;

Console.WriteLine("Simple Payroll System");

Staff staff = new Staff()//trightle couple 
{
    Id = "001",
    Name = "Su Su",
    NRC = "12/ThaKaNa(N)123456",
    BaseSalary = 100000
};
BankAccount bankAccount = new BankAccount()
{
    AccountNo = "123456",
    AccountName = "Su Su",
    OpeningBalance = 1000
};

//we can implement the code accroding to Use case or Scenario
staff.SetBankAcount(bankAccount);

staff.StaffDetail();
IPayrollService payrollService = new PayrollService();
decimal netPay = payrollService.CalcualteNatPay(staff.BaseSalary, 20, 30, 5000, 0);
Console.WriteLine($"Net Pay: {Math.Round(netPay, 2)}");
bankAccount.Deposit(netPay);
bankAccount.CheckBalance();
bankAccount.Withdraw(500);
Console.WriteLine("Exchange rate in SG:" + bankAccount.GetSGExchangeRate(100));
Console.WriteLine("*******************************");

BankAccount bankAccount1 = new BankAccount()
{
    AccountNo = "123456",
    AccountName = "Mya Mya",
};
Staff staff1=new Staff(bankAccount1)
{
    Id = "002",
    Name = "Mya Mya",
    BaseSalary = 200000 
};
staff1.StaffDetail();
decimal netPayOfMyaMya = payrollService.CalcualteNatPay(staff1.BaseSalary, 30, 30, 5000, 0);
Console.WriteLine($"Net Pay: {Math.Round(netPayOfMyaMya, 2)}");
bankAccount1.Deposit(netPayOfMyaMya);
bankAccount1.CheckBalance();
bankAccount1.Withdraw(500);
Console.WriteLine("Exchange rate in SG:" + bankAccount1.GetSGExchangeRate(100));

