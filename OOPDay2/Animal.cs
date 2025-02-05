
namespace OOPDay2
{
    public class Animal
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { 
                if(value == null)
                {
                    throw new ArgumentNullException("Name cannot be null");
                }
                name = value; 
                }
        }
        
    private int lifeSpan;
    public int LifeSpan
    {
        get { return lifeSpan; }
        set { 
            if(value < 0)
            {
                throw new ArgumentOutOfRangeException("LifeSpan cannot be negative");
            }
            lifeSpan = value; 
            }
    }
    override public string ToString()=> $"Name: {this.name}, LifeSpan: {LifeSpan}";

    public virtual void Eat()=> Console.WriteLine($"{this.name} is eating .");
    public virtual void Sleep()=> Console.WriteLine($"{this.name} is sleeping .");
}
}