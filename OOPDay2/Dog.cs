namespace OOPDay2
{
    public class Dog:Animal//is-a relatinship 
    {
        public void Bark()=> Console.WriteLine($"{base.Name} is barking .");   
        override public void Sleep()=> Console.WriteLine($"{base.Name} is sleeping in the sun .");
        override public void Eat()=> Console.WriteLine($"{base.Name} is eating meat .");
    }
}