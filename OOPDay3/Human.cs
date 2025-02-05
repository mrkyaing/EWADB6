namespace OOPDay3
{
    public class Human:Mammal
    {
        override public void Speak()
        {
            Console.WriteLine($"Hello! I am {base.name}");
        }

        public override void Eat()
        {
           Console.WriteLine("I am eating"+base.EatFoodType);
        }
    }
}