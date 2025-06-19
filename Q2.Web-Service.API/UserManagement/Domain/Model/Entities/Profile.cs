namespace Q2.Web_Service.API.UserManagement.Domain.Model.Entities;

using System.Collections.Generic;


    public class Profile
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Gender { get; }
        public IReadOnlyList<Address> Addresses { get; }

        public Profile(string firstName, string lastName, string gender, IReadOnlyList<Address> addresses)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Addresses = addresses;
        }

        public string GetFullName() => $"{FirstName} {LastName}".Trim();
    }
