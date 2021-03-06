﻿using TechChallenge.Domain.Common;

namespace TechChallenge.Domain.LeadManagement
{
    public class Category : Entity
    {
        public string Name { get; }
        public int ParentId { get; }

        public Category(string name, int parentId)
        {
            Name = name;
            ParentId = parentId;
        }

    }
}
