using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public class Person: IEntity<string>
    {
        public static string defaultId = "1";
        public Person()
        {
            Id = defaultId;
            FirstName = "John";
            LastName = "Doe";
            Number = 123;
            Date = DateTime.Parse("01/01/2014");
            Adr = new Address();

        }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Number { get; set; }
        public DateTime? Date { get; set; }
        public Address Adr { get; set; }

        //public override bool Equals(Object obj)
        //{

        //    if (obj == null) { return false; }
        //    if (!(obj is Person)) { return false; }

        //    Person p2 = (Person)obj;

        //    if (!Id.Equals(p2.Id)) { return false; }
        //    if (!FirstName.Equals(p2.FirstName)) { return false; }
        //    if (!LastName.Equals(p2.LastName)) { return false; }
        //    if (!Number.Equals(p2.Number)) { return false; }
        //    if (!Date.Equals(p2.Date)) { return false; }
        //    if (!Adr.Number.Equals(p2.Adr.Number)) { return false; }
        //    if (!Adr.Street.Equals(p2.Adr.Street)) { return false; }


        //    return true;
        //}
    }
    public class Address
    {
        public Address()
        {
            Number = 10;
            Street = "abc";
        }
        public int Number { get; set; }
        public string Street { get; set; }

    }
}