using System.Collections.Generic;
using TechChallenge.Domain.Common;

namespace TechChallenge.Domain.ValueObjects
{
    public class Amount: ValueObject
    {
        public double Value { get; }

        public Amount(double value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public Amount Discount(double discountPercent)
        {
            var newValue = Value * (100 - discountPercent) / 100;
            return new Amount(newValue);
        }
    }
}
