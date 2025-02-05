namespace OOPDay4
{
    //TextEditor : Visual Studio Code >>lightweight
    //IDE : Visual Studio  >>heavyweight >>provides more features(powerful)
    public class EnglishPeople : SayHello
    {
        public override void AboutMe()
        {
            //How is here
           Console.WriteLine("I am an English person.");
           Console.WriteLine("I speak English.");
           Console.WriteLine($"I am {base.Name}. I am from {base.Address}.");
        }
        public override void SayGoodMorning()
        {
            //When is here
            Console.WriteLine("Good Morning");
        }
        public override void SayGreetingMessage()
        {
            //What is here
            Console.WriteLine("Hi,Nice to see you.");
        }
    }
}