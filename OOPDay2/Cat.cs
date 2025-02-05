namespace OOPDay2
{
    public class Cat : Animal
    {
        public void CatchMouse() => Console.WriteLine($"{base.Name} is catching mouse .");
        override public void Sleep()
        {
            Console.WriteLine($"{base.Name} is sleeping in the BOX .");
        }
        override public void Eat()
        {
            Console.WriteLine($"{base.Name} is eating fish .");
        }
        
    }
}