namespace OOPDay2;

class Program
{
    static void Main(string[] args)
    {
        Cat cat = new Cat();
        cat.Name = "Tom";
        cat.LifeSpan = 5;
        Console.WriteLine(cat);
        cat.Sleep();//Tom is sleeping in the BOX .
        cat.Eat();//
        cat.CatchMouse();

        Dog dog = new Dog();
        dog.Name = "Spike";
        dog.LifeSpan = 10;
        Console.WriteLine(dog);//1235
        dog.Sleep();//Spike  is sleeping in the sun . 
        dog.Eat();
        dog.Bark();
    }
}
