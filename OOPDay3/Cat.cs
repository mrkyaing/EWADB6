namespace OOPDay3
{
    public class Cat:Mammal
    {
        public override void Speak()
        {
            Console.WriteLine(base.Name+"Meow!");
        }
    }
}