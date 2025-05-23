using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Rom.Domain.Abstractions.Validation;
using Rom.Domain.Validation;
using Rom.Domain.Abstractions.DataTransfer;
using System.Text.Json.Serialization;

namespace Rom.Domain.Base
{
    /// <summary>
    /// Base class for domain entities that supports validation logic.
    /// Provides automatic validation through data annotations, custom validation handling,
    /// and a list of formatted validation errors for serialization purposes.
    /// </summary>
    public abstract class RomBaseDomain : IRomValidatableDomain
    {
        [JsonIgnore]
        internal List<ValidationResult> _listCustomValidationResult = new List<ValidationResult>();

        /// <summary>
        /// Indicates whether the domain object is valid (has no validation errors).
        /// </summary>
        [NotMapped]
        public bool IsValidDomain => !ListValidationResult.Any();

        /// <summary>
        /// Returns the full list of validation results, combining annotation-based and custom results.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public List<ValidationResult> ListValidationResult
        {
            get
            {
                var results = ValidationDomain.ValidateDomain(this);

                if (_listCustomValidationResult.Any())
                {
                    results.AddRange(_listCustomValidationResult);
                }

                return results;
            }
        }

        /// <summary>
        /// Returns a simplified list of validation errors, including field name and error message.
        /// </summary>
        [NotMapped]
        public List<ValidationErrorDataTransfer> ListValidationError
        {
            get
            {
                var errorList = new List<ValidationErrorDataTransfer>();

                foreach (var result in ListValidationResult)
                {
                    // If no field is specified, consider a general error
                    if (result.MemberNames == null || !result.MemberNames.Any())
                    {
                        errorList.Add(new ValidationErrorDataTransfer
                        {
                            Field = string.Empty,
                            Message = result.ErrorMessage ?? string.Empty
                        });
                    }
                    else
                    {
                        foreach (var field in result.MemberNames)
                        {
                            errorList.Add(new ValidationErrorDataTransfer
                            {
                                Field = field,
                                Message = result.ErrorMessage ?? string.Empty
                            });
                        }
                    }
                }

                return errorList;
            }
        }

        /// <summary>
        /// Adds a custom validation result.
        /// </summary>
        private void AddValidationResult(ValidationResult validationResult)
        {
            _listCustomValidationResult.Add(validationResult);
        }

        /// <summary>
        /// Adds a validation error with associated fields.
        /// </summary>
        public void AddValidationResult(List<string> listFieldName, string message)
        {
            AddValidationResult(new ValidationResult(message, listFieldName));
        }

        /// <summary>
        /// Adds a validation error without a specific field.
        /// </summary>
        public void AddValidationResult(string message)
        {
            AddValidationResult(new ValidationResult(message));
        }
    }
}
