using System;
using System.Collections.Generic;
using System.Text;
using TechChallenge.Domain.Common;

namespace TechChallenge.Domain.ValueObjects
{
    public class Contact: ValueObject
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PhoneNumber { get; }
        
        public string EmailAddress { get; }

        public string FullName
        {
            get => $"{FirstName} {LastName}";
            private set
            {
                var values = value.Split(" ");
                if(values.Length < 2)
                    throw new ArgumentOutOfRangeException(nameof(value), "Name and surname expected");
                var sb = new StringBuilder();
                for (var i = 0; i < values.Length-1; i++)
                {
                    sb.Append($"{values[i]} ");
                }
                FirstName = sb.ToString().TrimEnd();
                LastName = values[^1];
            }

        } 

        public Contact(string firstName, string lastName, string phoneNumber, string emailAddress)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FirstName;
            yield return LastName;
            yield return PhoneNumber;
            yield return EmailAddress;
        }

        public void SetFullName(string fullName)
        {
            FullName = fullName;
        }
    }
}
