using Rom.Domain.Abstractions.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rom.Domain.Validation
{
    /// <summary>
    /// Static helper class to validate domain entities.
    /// </summary>
    public static class ValidationDomain
    {
        /// <summary>
        /// Performs data annotations validation on a domain object.
        /// </summary>
        public static List<ValidationResult> ValidateDomain(IRomValidatableDomain domainObject)
        {
            var context = new ValidationContext(domainObject, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(domainObject, context, results, true);

            return results;
        }
    }
}
