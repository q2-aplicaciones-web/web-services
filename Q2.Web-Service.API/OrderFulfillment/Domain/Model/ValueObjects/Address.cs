using System;

namespace Q2.Web_Service.API.OrderFulfillment.Domain.Model.ValueObjects
{
    public class Address
    {
        public string Street { get; }
        public string City { get; }
        public string Country { get; }
        public string State { get; }
        public string Zip { get; }

        public Address(string street, string city, string country, string state, string zip)
        {
            Street = street;
            City = city;
            Country = country;
            State = state;
            Zip = zip;
        }

        public override string ToString()
        {
            return $"{Street}, {City}, {State}, {Zip}, {Country}";
        }
    }
}
