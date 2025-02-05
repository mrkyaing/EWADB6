namespace OOPDay5
{
    public class Staff:IShowDetail
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NRC { get; set; }
        public decimal BaseSalary { get; set; }
        
        //for payroll process
        public decimal AttendanceDays { get; set; }
        
        //Has-A relationship
        private  BankAccount _bankAccount;
        
        public Staff(BankAccount bankAccount)
        {
            _bankAccount = bankAccount;
        }

        public Staff(){

        }
        public void SetBankAcount(BankAccount bankAccount){
            this._bankAccount = bankAccount;
        }

        public void StaffDetail()
        {
            Console.WriteLine($"Staff ID: {Id}");
            Console.WriteLine($"Staff Name: {Name}");
            Console.WriteLine($"Staff NRC: {NRC}");
            Console.WriteLine($"Staff Base Salary: {BaseSalary}");
        }

        
    }
}