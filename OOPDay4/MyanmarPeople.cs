namespace OOPDay4
{
    public class MyanmarPeople : SayHello
    {
        public override void AboutMe()
        {
            Console.WriteLine("ကျနော်က  မြန်မာကပါဗျာ.");
            Console.WriteLine("မြန်မာစကားကိုပြောပါပြီ.");
            Console.WriteLine($"I am {base.Name}. I am from {base.Address}.");
        }

        public override void SayGoodMorning()
        {
           Console.WriteLine("မင်္ဂလာ မနက်ခင်းပါ");
        }

        public override void SayGreetingMessage()
        {
            Console.WriteLine("မင်္ဂလာပါ");
        }
    }
}