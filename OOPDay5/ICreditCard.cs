namespace OOPDay5;
    public interface ICreditCard
    {
        decimal GetSGExchangeRate(decimal inputAmount);
        decimal GetUSExchangeRate(decimal inputAmount);
        decimal GetBahtExchangeRate(decimal inputAmount);
    }
