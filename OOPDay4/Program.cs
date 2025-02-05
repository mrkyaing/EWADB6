using OOPDay4;

Console.WriteLine("=====================================");
SayHello englishPeople = new EnglishPeople(){
    Name = "John",
    Address = "England"
};
englishPeople.AboutMe();
englishPeople.SayGreetingMessage();
englishPeople.LiveIn();

Console.WriteLine("=====================================");
SayHello japanesePeople = new JapanesePeople(){
    Name = "Taro",
    Address = "Japan"
};
japanesePeople.AboutMe();
japanesePeople.SayGreetingMessage();
japanesePeople.LiveIn();

Console.WriteLine("=====================================");
SayHello myanmarPeople = new MyanmarPeople(){
    Name = "Aung Aung",
    Address = "Myanmar"
};
myanmarPeople.AboutMe();
myanmarPeople.SayGreetingMessage();
myanmarPeople.LiveIn();
Console.WriteLine("=====================================");