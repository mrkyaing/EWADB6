namespace OOPDay5
{
    public class BankAccount:ICreditCard
    {
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public decimal OpeningBalance { get; set; }//1000

        public void Deposit(decimal amount)//100
        {
            if(amount < 0)
            {
                throw new Exception("Amount must be greater than 0");
            }
            OpeningBalance += amount;//1100
        }
        public void Withdraw(decimal amount)//100
        {
            if(amount < 0)
            {
                throw new Exception("Amount must be greater than 0");
            }
            if(amount > OpeningBalance)
            {
                throw new Exception("Insufficient balance");
            }
            OpeningBalance -= amount;//1000
        }

        public void CheckBalance()=>Console.WriteLine($"Your Balance: {Math.Round(OpeningBalance,2)}");

        public decimal GetSGExchangeRate(decimal inputAmount){
            if(inputAmount < 0)
            {
                throw new Exception("Amount must be greater than 0");
            }
          return  inputAmount * 3600.7m;
        }
        public decimal GetUSExchangeRate(decimal inputAmount){
            if(inputAmount < 0)
            {
                throw new Exception("Amount must be greater than 0");
            }
            return inputAmount * 4500.30m;
        }
        public decimal GetBahtExchangeRate(decimal inputAmount){
            if(inputAmount < 0)
            {
                throw new Exception("Amount must be greater than 0");
            }
            return inputAmount * 130.2m;
        }

    }
}