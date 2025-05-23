namespace Rom.Domain.Abstractions.DataTransfer
{
    /// <summary>
    /// Data Transfer Object representing a validation error.
    /// </summary>
    public class ValidationErrorDataTransfer
    {
        public string Field { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;
    }
}
