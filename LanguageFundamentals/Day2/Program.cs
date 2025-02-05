using System.Globalization;
using System.Text.RegularExpressions;

namespace Day2;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Car Mycar = new Car("Toyota", "Red", 5);
            Console.WriteLine(Mycar);//Car Information:[Toyata:Red:5]
            Car yourCar = new Car("Honda", "Blue", -1);
            Console.WriteLine(Mycar);//it will occur exception
        }
        catch (Exception e)
        {
            Console.WriteLine("Error : " + e.Message);//Gear No is required
        }

        int i = 10;
        int j = 10;
        if (i > j)
        {
            Console.WriteLine("i is greater than j.I am inside in if block");
        }
        else
        {
            Console.WriteLine("I am in else block");
        }
        int result = ((2 + 3) * 6 - 3) / 1;//27
        Console.WriteLine("Result is : " + result);
        int[] marks = { 90, 80, 70, 60, 50, 10 };//6
        // Console.WriteLine("printing out with for loop");
        // for(int k=0;k<marks.Length;k++){
        //     Console.WriteLine("Marks are : "+marks[k]);//90 
        // }
        Console.WriteLine("printing out with for loop each");
        foreach (var item in marks)
        {
            switch (item)
            {//90
                case 50: goto SayHello;
                default: Console.WriteLine("Marks are : " + item); break;//90
            }
        }
        
    //Label statement in C#
    MyFriend:
        {
            Console.WriteLine("printing out with for loop each");
            string[] friends = { "Aye Aye", "Su Su", "Jame", "Smith", "Raj", "Ravi" };
            foreach (var friend in friends)
            {
                Console.WriteLine("My friend is : " + friend);
            }
            return;
        }
    //Label statement in C#
    SayHello:
        {
            Console.WriteLine("Hello, World!");
            goto MyFriend;
        }
    }
}
