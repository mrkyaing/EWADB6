namespace OOPDay4
{
    public abstract class SayHello
    {
        public string Name { get; set; }
        public string Address { get; set; }
        
        //Non-Abstract Method
        public void LiveIn()=> Console.WriteLine($"I live in {Address}.");
        public abstract void SayGreetingMessage();//Say WHAT
        public abstract void AboutMe();//Say WHAT
        
        //Non-Abstract Method
        public void SayGoodBye() => Console.WriteLine("Good Bye");
        //Abstract Method
        public abstract void SayGoodMorning();
    }
}