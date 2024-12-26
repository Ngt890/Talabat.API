using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.OrderAggregate
{
    public class Adress
    {
        public Adress()
        {
        }
        public Adress(string firstName, string lastName, string street, string city, string country)
        {
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            City = city;
            Country = country;
        }

        public  string  FirstName { get; set; }
        public string LastName { get; set; }
        public string Street{ get; set; }
        public string City { get; set; }
        public string Country { get; set; }


    }
}
