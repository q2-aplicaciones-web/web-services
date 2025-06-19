namespace Q2.Web_Service.API.UserManagement.Domain.Model.Entities;
    public class Address
    {
        public Guid Id { get; }
        public Guid ProfileId { get; }
        public string StreetAddress { get; }
        public string City { get; }
        public string State { get; }
        public string Zip { get; }
        public string Country { get; }

        public Address(Guid id, Guid profileId, string streetAddress, string city, string state, string zip, string country)
        {
            Id = id;
            ProfileId = profileId;
            StreetAddress = streetAddress;
            City = city;
            State = state;
            Zip = zip;
            Country = country;
        }

        public string FormatAddress()
        {
            var parts = new[] { StreetAddress, City, State, Zip, Country };
            return string.Join(", ", parts.Where(p => !string.IsNullOrWhiteSpace(p)));
        }
    }
