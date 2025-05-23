using Rom.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rom.Domain.Test.Base
{
    // Concrete class to test the abstract RomBaseDomain
    public class TestDomain : RomBaseDomain
    {
        [Required]
        public string Name { get; set; }

        [Range(1, 100)]
        public int Age { get; set; }
    }

    public class RomBaseDomainTest
    {
        [Fact]
        public void IsValidDomain_ShouldReturnTrue_WhenNoValidationErrors()
        {
            var entity = new TestDomain { Name = "Valid", Age = 30 };

            Assert.True(entity.IsValidDomain);
            Assert.Empty(entity.ListValidationError);
        }

        [Fact]
        public void IsValidDomain_ShouldReturnFalse_WhenCustomValidationErrorsExist()
        {
            var entity = new TestDomain { Name = "Test", Age = 50 };
            entity.AddValidationResult("Custom error");

            Assert.False(entity.IsValidDomain);
            Assert.Contains(entity.ListValidationError, e => e.Message == "Custom error");
        }

        [Fact]
        public void ListValidationError_ShouldReturnFieldAndMessage_ForEachValidationResult()
        {
            var entity = new TestDomain(); // Name is required, Age is invalid

            var errors = entity.ListValidationError;

            Assert.False(entity.IsValidDomain);
            Assert.Contains(errors, e => e.Field == "Name");
        }

        [Fact]
        public void ListValidationResult_ShouldIncludeCustomValidation()
        {
            var entity = new TestDomain { Name = "João", Age = 25 };
            entity.AddValidationResult("Erro de teste");

            var results = entity.ListValidationResult;

            Assert.Contains(results, r => r.ErrorMessage == "Erro de teste");
        }

        [Fact]
        public void AddValidationResult_ShouldAddCustomError()
        {
            var entity = new TestDomain();
            entity.AddValidationResult(new List<string> { "CampoX" }, "Mensagem personalizada");

            var error = entity.ListValidationError.FirstOrDefault(e => e.Field == "CampoX");

            Assert.NotNull(error);
            Assert.Equal("Mensagem personalizada", error.Message);
        }
    }
}
