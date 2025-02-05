namespace OOPDay4
{
    public class JapanesePeople : SayHello
    {
        public override void AboutMe()
        {
            Console.WriteLine("I am a Japanese person.");
            Console.WriteLine("I speak Japanese.");
            Console.WriteLine($"I am {base.Name}. I am from {base.Address}.");
        }

        public override void SayGoodMorning()
        {
            Console.WriteLine("おはようございます");
        }

        public override void SayGreetingMessage()
        {
            Console.WriteLine("こんにちは、お元気ですか");
        }
    }
}