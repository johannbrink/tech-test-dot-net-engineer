using TechChallenge.Domain.Common;

namespace TechChallenge.Domain.LeadManagement
{
    public class Suburb : Entity
    {
        public string Name { get; }
        public string PostCode { get; }

        public Suburb(string name, string postCode)
        {
            Name = name;
            PostCode = postCode;
        }

    }
}
