using OOPDay3;
Console.WriteLine("******************");
Console.WriteLine("Hello, World!");
Human human = new Human(){
Name="MG",
EatFoodType="Veg",
Color="Brown",
LifeSpan=20
};
human.Speak();// Hello! I am MG 
human.Eat();
human.Sleep();
Console.WriteLine(human);
Console.WriteLine("******************");
Dog dog = new Dog(){
Name="Tommy",
EatFoodType="Meat",
Color="Black",
LifeSpan=5
};
dog.Speak();//Woof!
Console.WriteLine("******************");

Cat cat = new Cat(){
Name="Kitty",
EatFoodType="Fish",
Color="yellow",
LifeSpan=20
};
cat.Speak();//Meow!
Console.WriteLine("******************");