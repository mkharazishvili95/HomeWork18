using System;

namespace HomeWork18.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobPosition { get; set; }
        public double Salary { get; set; }
        public double WorkExperience { get; set; }
        public DateTime CreateDate { get; set; }
        public Address PersonAddress { get; set; }
    }
}
