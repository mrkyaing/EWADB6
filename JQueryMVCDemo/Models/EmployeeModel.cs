namespace JQueryMVCDemo.Models
{
    public class EmployeeModel
    {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName {get;set;}
            //Has-Relationship in here for Home Address 
            public Address HomeAddress { get; set; }
    }
}