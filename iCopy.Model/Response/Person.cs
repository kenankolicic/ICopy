using System;

namespace iCopy.Model.Response
{
    public class Person
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public int CityId { get; set; }
        public Model.Response.City City { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
