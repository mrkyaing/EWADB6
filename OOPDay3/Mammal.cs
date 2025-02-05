namespace OOPDay3
{
    public class Mammal
    {
    protected string name;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }//property with full property
    
    public string EatFoodType { get; set; }//property short property
    public string Color { get; set; }
    public int LifeSpan { get; set; }


     public virtual void Eat()=>Console.WriteLine("Mammal is eating.");

     public virtual void Sleep()=>Console.WriteLine("Mammal is sleeping.");

     public virtual void Speak()=>Console.WriteLine("Mammal is Speaking.");


     override public string ToString()
     {
         return $"Name: {this.name}, Eat Food Type: {EatFoodType}, Color: {Color}, LifeSpan: {LifeSpan}";
     }

    }
}