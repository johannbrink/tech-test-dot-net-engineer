using FluentAssertions;
using TechChallenge.Domain.ValueObjects;
using Xunit;

namespace TechChallenge.Tests.Domain
{
    public class ValueObjectTests
    {
        [Fact]
        public void Amount_Value_Matches_Input()
        {
            var amount = new Amount(1);
            amount.Value.Should().Be(1);
        }

        [Fact]
        public void Contact_Props_Matches_Input()
        {
            // Arrange
            const string expectedFirstName = "first";
            const string expectedLastName = "last";
            const string expectedFullName = "first last";
            const string expectedPhoneNumber = "phone";
            const string expectedEmail = "email";

            // Act
            var contact = new Contact(expectedFirstName, expectedLastName, expectedPhoneNumber, expectedEmail);

            // Assert
            contact.FirstName.Should().Be(expectedFirstName);
            contact.LastName.Should().Be(expectedLastName);
            contact.FullName.Should().Be(expectedFullName);
            contact.EmailAddress.Should().Be(expectedEmail);
            contact.PhoneNumber.Should().Be(expectedPhoneNumber);
        }

        

        [Theory]
        [InlineData("new-first new-last", "new-first", "new-last")]
        [InlineData("new-first new-middle new-last", "new-first new-middle", "new-last")]
        public void Contact_FullName_Yields_Expected(string fullName, string expectedFirstName, string expectedLastName)
        {
            // Arrange
            const string expectedPhoneNumber = "phone";
            const string expectedEmail = "email";

            var contact = new Contact("old-first", "old-last", expectedPhoneNumber, expectedEmail);

            // Act
            contact.SetFullName(fullName);

            // Assert
            contact.FirstName.Should().Be(expectedFirstName);
            contact.LastName.Should().Be(expectedLastName);
        }

    }
}
