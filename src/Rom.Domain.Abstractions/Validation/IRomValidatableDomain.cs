using Rom.Domain.Abstractions.DataTransfer;
using System.Collections.Generic;

namespace Rom.Domain.Abstractions.Validation
{
    /// <summary>
    /// Contract for a validatable domain object.
    /// </summary>
    public interface IRomValidatableDomain
    {
        /// <summary>
        /// Indicates whether the domain object is valid.
        /// </summary>
        bool IsValidDomain { get; }

        /// <summary>
        /// List of validation errors, containing field name and message.
        /// </summary>
        List<ValidationErrorDataTransfer> ListValidationError { get; }
    }
}
