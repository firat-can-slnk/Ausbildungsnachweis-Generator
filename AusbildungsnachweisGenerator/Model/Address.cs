using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AusbildungsnachweisGenerator.Model
{
    public class Address
    {
        public Address()
        {

        }
        public Address(string street, string houseNumber, string city, string postalCode)
        {
            Street = street;
            HouseNumber = houseNumber;
            City = city;
            PostalCode = postalCode;
        }

        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string FullAddress => $"{Street} {HouseNumber}, {PostalCode} {City}";
    }
}
