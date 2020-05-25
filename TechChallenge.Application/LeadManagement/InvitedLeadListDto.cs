using System;

namespace TechChallenge.Application.LeadManagement
{
    public class InvitedLeadListDto
    {
        public int Id { get; set; }
        public string Suburb { get; set; }
        public string PostCode { get; set; }
        public string Category { get; set; }
        public string Contact { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
}