

using StudentInfo;
using TeacherInfo;

namespace OOPDay1;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        
        Engine engine=new Engine();
        engine.OilLevel=10;
        engine.GearNo="P";
        
        Car myCar=new Car(engine);//set the Engine to the Car
        myCar.Model="Toyota";//. Operator to access states and behaviors of class 
        myCar.Color="Red";
        Car.UseBreak();
        myCar.LicenceNo="YGN-303";
        myCar.PlayHorn(3);
        Console.WriteLine(myCar);
        
        Car yourCar=new Car(engine); 
        yourCar.Model="Honda";
        yourCar.Color="Blue";
        yourCar.LicenceNo="YGN-304";
    
        yourCar.PlayHorn(2);
        yourCar.PushLever();
        Console.WriteLine(yourCar);
        Car.UseBreak();
        
        
        
        Console.WriteLine("Show the result for static usage");
        Utility.ApplicationUser();
        Utility.PrintTime();
        Utility.Today();
        Console.WriteLine("hello");
        Console.WriteLine(Utility.WeekDays);
        Console.WriteLine(1);
        Console.WriteLine(true);

        Teacher teacher=new Teacher();
        teacher.Name="U Ba";
        teacher.Subject="Math";
        Console.WriteLine(teacher);
        Console.WriteLine(teacher.sayHello());
        Teacher.ShowTeacherInfo();

        Student student=new Student();
        student.SetName("Aung Aung");
        student.SetAge(20);
        student.Email="aung@gmail.com";
        Student.ShowStudentInfo();
        Console.WriteLine(student);
       
        Student s1=new Student();
        Student.ShowStudentInfo();
        s1.SetName("susu");
        s1.SetAge(20);
        s1.Email="susu@gmail.com";
        Console.WriteLine(s1);
    }
}
