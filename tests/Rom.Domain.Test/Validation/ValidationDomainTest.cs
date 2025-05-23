using Rom.Domain.Base;
using Rom.Domain.Validation;
using System.ComponentModel.DataAnnotations;

namespace Rom.Domain.Test.Validation
{    
    public class ValidationDomainTest
    {
        private class ValidDomain : RomBaseDomain
        {
            [Required]
            public string Name { get; set; } = "Test";

            [Range(1, 100)]
            public int Age { get; set; } = 25;
        }

        private class InvalidDomain : RomBaseDomain
        {
            [Required]
            public string Name { get; set; }

            [Range(1, 100)]
            public int Age { get; set; } = 150; // Invalid
        }

        [Fact]
        public void ValidateDomain_ShouldReturnEmptyList_WhenObjectIsValid()
        {
            var obj = new ValidDomain();

            var results = ValidationDomain.ValidateDomain(obj);

            Assert.Empty(results);
        }

        [Fact]
        public void ValidateDomain_ShouldReturnValidationErrors_WhenObjectIsInvalid()
        {
            var obj = new InvalidDomain(); // Missing Name and invalid Age

            var results = ValidationDomain.ValidateDomain(obj);

            Assert.Equal(2, results.Count);
            Assert.Contains(results, r => r.MemberNames.Contains("Name"));
            Assert.Contains(results, r => r.MemberNames.Contains("Age"));
        }

        [Fact]
        public void ValidateDomain_ShouldReturnCorrectErrorMessages()
        {
            var obj = new InvalidDomain();

            var results = ValidationDomain.ValidateDomain(obj);

            var nameError = results.FirstOrDefault(r => r.MemberNames.Contains("Name"));
            var ageError = results.FirstOrDefault(r => r.MemberNames.Contains("Age"));

            Assert.NotNull(nameError);
            Assert.NotNull(ageError);

            Assert.False(string.IsNullOrWhiteSpace(nameError.ErrorMessage));
            Assert.False(string.IsNullOrWhiteSpace(ageError.ErrorMessage));
        }

    }
}
