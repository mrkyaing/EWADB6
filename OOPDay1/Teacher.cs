namespace TeacherInfo
{
    public class Teacher
    {
        public string Name;
        public string Subject;

        public string sayHello(){
            return "Hello";
        }
        
        public static void ShowTeacherInfo()
        {
            Console.WriteLine("I am a teacher come from teacher info namespace.");
        }
        override public string ToString()=>$"I am {Name}, I teach {Subject} subject.";
    }
}