
namespace StudentInfo
{
    public class Student
    {
        private string name ;
        private int age;
        
        private string email;

        public string Email{
            get{
                return email;
            }
            //data protection for email
            set{
                if(string.IsNullOrEmpty(value)){
                    throw new Exception("Email must not be empty");
                }
                if(!value.Contains("@")){
                    throw new Exception("Email must contain @ in your value.");
                }
                email=value;
            }
        }

        public  static void ShowStudentInfo()
        {
            Console.WriteLine("I am a student come from student info namespace.");
        }

        public void SetName(string name)
        {
            if(string.IsNullOrEmpty(name)){
                throw new Exception("Name must not be empty");
            }
            this.name=name;
        }

        public void SetAge(int age)
        {
            if(age<0){
                throw new Exception("Age must be greater than 0");
            }
            this.age=age;
        }

        override public string ToString()
        {
            return $"Name: {this.name}, Age: {this.age} ,Email:{this.email}";
        }
    }
}